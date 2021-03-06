delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `AddPlan`(argID int, argSeqNumber int, 
						argProductID int, argOptionsID int, argAgentName varchar(45))
BEGIN
	DECLARE locAgentID int;
	
	IF argID = -1 OR argSeqNumber = -1 THEN
		set locAgentID = (select a.ID from Agents as a
							where a.Name = argAgentName);
		IF argSeqNumber = -1 THEN
			IF EXISTS(select max(SeqNumber) from Plans
								where AgentID=locAgentID and NOT ISNULL(CategoryID)
								group by NOT ISNULL(CategoryID)) THEN

				set argSeqNumber = (select max(SeqNumber) from Plans
									where AgentID=locAgentID and NOT ISNULL(CategoryID)
									group by NOT ISNULL(CategoryID)) + 1;
			ELSE
				set argSeqNumber = 1;
			END IF;
		END IF;
		set argID = (select ID from Plans
						where AgentID = locAgentID and SeqNumber = argSeqNumber);
	END IF;

	UPDATE Plans
	SET SeqNumber = argSeqNumber, CategoryID = argProductID, 
		OptionsReceivingProductID = argOptionsID
	WHERE ID = argID;

END$$

