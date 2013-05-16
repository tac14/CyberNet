delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `Eat`(argAgentID int)
BEGIN
	DECLARE MaxEnergy decimal(5,2);
	DECLARE AddEnergy decimal(5,2);
	DECLARE dEnergy decimal(5,2);
	DECLARE Surplus decimal(17,2);
	DECLARE i int; 
	DECLARE StockID int;
		
	select ROUND((100-a.Energy)/2,2)
	into @MaxEnergy
	from Agents as a
	where a.ID = argAgentID;

	/*select @MaxEnergy as Me;*/

	SET i=0;
	label1: LOOP
		SET i = i + 1;
		IF @MaxEnergy > 0 and 
				exists (select * from Stock as s 
							join Categories as c on c.ID = s.CategoryID
							where s.AgentID = argAgentID and c.EnergyIndex <> 0) THEN

			select a.Count - ROUND(@MaxEnergy/(a.EnergyIndex*a.Quality/100),2), 
					@MaxEnergy,
					@MaxEnergy - ROUND(a.Count*(a.EnergyIndex*a.Quality/100),2),
					a.ID, ROUND(a.Count*(a.EnergyIndex*a.Quality/100),2)
			into @Surplus, @AddEnergy, @MaxEnergy, @StockID, @dEnergy
			from
				(select  s.Count, c.EnergyIndex, s.Quality, s.ID,
						@k:=@k+1 as k
				from Stock as s
					join Categories as c on c.ID = s.CategoryID,
					(SELECT @k:= 0) AS k
				where s.AgentID = argAgentID and c.EnergyIndex <> 0) as a
			where a.k = 1;

			/*select @MaxEnergy as Me, @AddEnergy as AE;*/

			IF @Surplus<=0 THEN
				DELETE FROM Stock
				WHERE ID=@StockID;

				UPDATE Agents
				SET Energy = Energy + @dEnergy
				WHERE ID = argAgentID;
			ELSE
				UPDATE Stock
				SET Count = @Surplus
				WHERE ID=@StockID;

				UPDATE Agents
				SET Energy = Energy + @AddEnergy
				WHERE ID = argAgentID;
			END IF;


			set @dEnergy = 0;

		  ITERATE label1;
		END IF;
		LEAVE label1;
	  END LOOP label1;


END$$

