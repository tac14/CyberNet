delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `CalcStep`(argAgentName varchar(45))
BEGIN
	DECLARE locAgentID int;
	DECLARE locAgentCityID int;
	DECLARE locProductID int;
	DECLARE locReceivingProductID int;
	DECLARE locProductReceiveIndex decimal(17,4);
	DECLARE i int; 
	DECLARE dEnergy decimal(5,2);
	DECLARE dHealth decimal(5,2);
	DECLARE dForce decimal(10,2);
	DECLARE dIntelligence decimal(10,2);
	DECLARE locRawID int;
	DECLARE locActionID int;
	DECLARE dCount decimal(17,4);
	DECLARE dQuality decimal(17,2);
	DECLARE CountIndex decimal(17,2);


	set @locAgentID = (select a.ID from Agents as a
						where a.Name = argAgentName);
	set @locAgentCityID = (select a.CityID from Agents as a
						where a.Name = argAgentName);

	set @locProductID = (select CategoryID
						from Plans
						where AgentID = @locAgentID and SeqNumber = 1);

	set @locProductReceiveIndex = (select ReceiveIndex
						from Categories
						where ID = @locProductID);

	set @locReceivingProductID = (select OptionsReceivingProductID
						from Plans
						where AgentID = @locAgentID and SeqNumber = 1);


		SET @i=0;
		label1: LOOP
			SET @i = @i + 1;
			IF @i < GetRawCountProductCreate (@locProductID, @locReceivingProductID) + 1 THEN

				call GetRawProductCreate (@locProductID, @locReceivingProductID, @i, 
											@locRawID, @locActionID, @CountIndex);
				/*select @locProductID, @locReceivingProductID, @i, @locRawID, 
							@locActionID, @CountIndex, IsProduct(@locRawID);*/
				IF IsProduct(@locRawID) = 0 THEN
					IF @locActionID = 2 THEN
						UPDATE Agents
						SET Cheerfulness = 125
						WHERE ID = @locAgentID;
					ELSE 
						IF @locActionID = 3 THEN
							UPDATE Agents
							SET Health = Health + (RAND()*7 - 3)
							WHERE ID = @locAgentID;
						ELSE
							IF not exists(select * from CategoriesPrevalence as cp
											where cp.CategoryID = @locRawID and cp.CityID = @locAgentCityID)  THEN
							
								INSERT CategoriesPrevalence (CategoryID, CityID,
														Prevalence, PrevalenceFluctuation,
														QualityNorm, QualityFluctuation)
								VALUES (@locRawID, @locAgentCityID, 
											ROUND(RAND()*100 ,2), ROUND(RAND()*20 ,2),
											ROUND(RAND()*100 ,2), ROUND(RAND()*20 ,2));
							END IF;


							select ROUND( ((cp.Prevalence + ((RAND() * cp.PrevalenceFluctuation * 2) - cp.PrevalenceFluctuation))/100) 
										* ((ag.Force * (RAND() * a.FMod/2)/100) + (ag.Intelligence * (RAND() * a.IMod/2)/100))
										* @locProductReceiveIndex * a.TehModifier , 4),
									ROUND( ((cp.QualityNorm + ((RAND() * cp.QualityFluctuation * 2) - cp.QualityFluctuation))/100) 
										* ((ag.Force * (RAND() * a.FMod/2)/100) + (ag.Intelligence * (RAND() * a.IMod/2)/100))
										* a.TehModifier, 2)
							into @dCount, @dQuality
							from CategoriesPrevalence as cp
								join Categories as c on c.ID = cp.CategoryID
								join Actions as a on a.ID = @locActionID
								join Agents as ag on ag.ID = @locAgentID
							where cp.CategoryID = @locRawID and cp.CityID = @locAgentCityID;

							call AddStock(@locAgentID, @locProductID, @dCount, @dQuality);
						END IF;
					END IF;
				ELSE
					/*TODO*/
					select s.ID, SUM(s.Count), 
							ROUND(@CountIndex * 
							CalcProductReceiveIndex(argAgentName, argProductID, argReceivingProductID)  
							, 2) as RawCount
					from Stock as s
							join Categories as c on c.ID = s.CategoryID
					where CategoryID = @locRawID
					group by CategoryID;

				END IF;

				ITERATE label1;
			END IF;
			LEAVE label1;
		  END LOOP label1;



	select 	ROUND(SUM((EnergyIndex/1000)*10),2), 
			ROUND(SUM(DangerIndex * RAND() * 0.8),2), 
			ROUND(AVG(ForceIndex * RAND() * 0.8),2), 
			ROUND(AVG(IntelligenceIndex * RAND() * 0.8),2),
			1 as u
	into	@dEnergy, @dHealth, @dForce, @dIntelligence , @i
	from Actions as a2,
			(select a.OptionsID, a.ActionID
			from OptionsReceivingProduct as a
			where a.ToID = @locProductID and a.FromID <> 0  and
					a.OptionsID = @locReceivingProductID
			union
			select a.OptionsID, a.ActionID
			from OptionsReceivingProduct as a
					join OptionsConditionsActionExe as e on a.FromOptionsID = e.OptionID
			where a.ToID = @locProductID and
					a.OptionsID = @locReceivingProductID) as b2
	where a2.ID=b2.ActionID
	group by u;


	UPDATE Agents
	SET Energy = ROUND(Energy - @dEnergy,2),
		Health = ROUND(Health - @dHealth - IF (Energy < 50, 2.5, 0) - IF (Cheerfulness = 0, 5, 0),2),
		Cheerfulness = Cheerfulness - 25,
		Agents.Force = IF(Cheerfulness <>0, ROUND(Agents.Force + @dForce  
					- IF (Energy < 30, 1.0, 0) - IF (Health < 50, 2.5, 0) ,2), Agents.Force),
		Intelligence = IF(Cheerfulness <>0, ROUND(Intelligence + @dIntelligence 
					- IF (Energy < 30, 0.5, 0) - IF (Health < 50, 1.0, 0),2), Intelligence)
	WHERE ID = @locAgentID;

	UPDATE Agents
	SET Energy = IF(Energy<0, 0, IF(Energy>100, 100, Energy)),
		Health = IF(Health<0, 0, IF(Health>100, 100, Health)),
		Cheerfulness = IF(Cheerfulness<0, 0, IF(Cheerfulness>100, 100, Cheerfulness)),
		Agents.Force = IF(Agents.Force<1, 1, Agents.Force),
		Intelligence = IF(Intelligence<1, 1, Intelligence)
	WHERE ID = @locAgentID;

	call Eat(@locAgentID);

	call NextTime();
END$$

