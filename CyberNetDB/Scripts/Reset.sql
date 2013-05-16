delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `Reset`(argAgentName varchar(45))
BEGIN
	DECLARE locAgentID int;

	set @locAgentID = (select a.ID from Agents as a
						where a.Name = argAgentName);

	DELETE FROM Stock
	WHERE AgentID = @locAgentID;

	DELETE FROM Plans
	WHERE AgentID = @locAgentID;
	
	UPDATE Agents
	SET Enegry = 100, Health = 100, Cheerfulness = 100, Agents.Force = 1, Intelligence =1
	WHERE ID =  @locAgentID;

END$$

