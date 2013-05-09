delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAgentState`(argName varchar(45))
BEGIN
	DECLARE AgentCount int;
	set AgentCount = (select COUNT(*)
	from Agents as a
	where a.Name = argName);

	IF AgentCount = 0 THEN
		insert Agents (Name)
		VALUE (argName);
	END IF;
	select a.Energy, a.Health, a.Force, a.Intelligence
	from Agents as a
	where a.Name = argName;
	

END$$

