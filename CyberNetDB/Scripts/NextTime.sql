delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `NextTime`()
BEGIN

	UPDATE Plans
	SET SeqNumber = -1
	WHERE SeqNumber = 1;

	UPDATE Plans
	SET SeqNumber = SeqNumber - 1
	WHERE SeqNumber <> -1;

	UPDATE Plans
	SET SeqNumber = 120, CategoryID = NULL, OptionsReceivingProductID = NULL
	WHERE SeqNumber = -1;

	UPDATE World
	SET CurrentDate = DATE_ADD(CurrentDate, INTERVAL 6 HOUR)
	WHERE ID = 1;

END$$

