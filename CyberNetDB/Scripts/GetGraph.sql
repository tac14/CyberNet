delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `GetGraph`(argCategoryID int)
BEGIN
	select a.OptionsID, c.Name, ac.Name, 1 as CatType
	from OptionsReceivingProduct as a
			left outer join Categories as c on c.ID = a.FromID
			left outer join Actions as ac on a.ActionID = ac.ID
	where a.ToID = argCategoryID and a.FromID <> 0
	union
	select a.OptionsID, c2.Name, ac.Name, 1 as CatType
	from OptionsReceivingProduct as a
			join OptionsConditionsActionExe as e on a.FromOptionsID = e.OptionID
			left outer join Categories as c2 on c2.ID = e.CategoryID
			left outer join Actions as ac on a.ActionID = ac.ID
	where a.ToID = argCategoryID;

END$$

