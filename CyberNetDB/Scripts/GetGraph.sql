delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `GetGraph`(argCategoryID int, argOptionsID int)
BEGIN
	IF argOptionsID = -1 THEN
		set argOptionsID = (select min(OptionsID) from OptionsReceivingProduct 
							where ToID = argCategoryID);
	END IF;

	select a.OptionsID, c.Name, ac.Name as ActionName, 1 as CatType
	from OptionsReceivingProduct as a
			left outer join Categories as c on c.ID = a.FromID
			left outer join Actions as ac on a.ActionID = ac.ID
	where a.ToID = argCategoryID and a.FromID <> 0  and
			1 = case when argOptionsID = 0 then 1
					 when a.OptionsID = argOptionsID then 1
					else 0 end
	union
	select a.OptionsID, c2.Name, ac.Name as ActionName, 1 as CatType
	from OptionsReceivingProduct as a
			join OptionsConditionsActionExe as e on a.FromOptionsID = e.OptionID
			left outer join Categories as c2 on c2.ID = e.CategoryID
			left outer join Actions as ac on a.ActionID = ac.ID
	where a.ToID = argCategoryID and
			1 = case when argOptionsID = 0 then 1
					 when a.OptionsID = argOptionsID then 1
					else 0 end;

END$$

