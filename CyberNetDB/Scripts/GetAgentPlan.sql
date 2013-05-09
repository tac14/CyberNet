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
			IF i < 101 THEN
				insert Plans (AgentID, SeqNumber)
				VALUE (locAgentID, i);

			  ITERATE label1;
			END IF;
			LEAVE label1;
		  END LOOP label1;

	END IF;

	select distinct p.ID, p.SeqNumber, 
			IFNULL(c.ID, 0) as ProductID,
			IFNULL(c.Name, '') as ProductName, 
			IFNULL(o.OptionsID, 0) as OptionsID
	from Plans as p
			left outer join Categories as c on c.ID = p.CategoryID
			left outer join OptionsReceivingProduct as o on o.OptionsID = p.OptionsReceivingProductID
	where p.AgentID = locAgentID;
END$$

