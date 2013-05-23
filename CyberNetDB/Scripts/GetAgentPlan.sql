delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAgentPlan`(argAgentName varchar(45))
BEGIN
	DECLARE locAgentID int;
	DECLARE PlanCount int; 
	DECLARE i int; 

	set locAgentID = (select a.ID
	from Agents as a
	where a.Name = argAgentName);

	set PlanCount = (select COUNT(*)
	from Plans as p
	where p.AgentID = locAgentID);

	IF PlanCount = 0 THEN
		SET i=0;
		label1: LOOP
			SET i = i + 1;
			IF i < 121 THEN
				insert Plans (AgentID, SeqNumber)
				VALUE (locAgentID, i);

			  ITERATE label1;
			END IF;
			LEAVE label1;
		  END LOOP label1;

	END IF;

	select  p.ID, p.SeqNumber, 
			DATE_ADD(GetCurrentTime(), INTERVAL 6*p.SeqNumber HOUR) as PlanDate,
			IFNULL(c.ID, 0) as ProductID,
			IFNULL(c.Name, '') as ProductName, 
			IFNULL(o.OptionsID, 0) as OptionsID,
			GetVariantNumber (IFNULL(c.ID, 0), IFNULL(o.OptionsID, 0)) as VariantNumber,
			GetComposition(argAgentName, c.ID, o.OptionsID) as Composition
	from Plans as p
			left outer join Categories as c on c.ID = p.CategoryID
			left outer join (select distinct OptionsID from OptionsReceivingProduct) as o 
					on OptionsID = p.OptionsReceivingProductID
	where p.AgentID = locAgentID
	order by p.SeqNumber;
END$$

