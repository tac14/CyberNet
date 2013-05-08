delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAgentPlan`(argAgentID int)
BEGIN
	select p.SeqNumber, c2.Name as NecessaryName, c1.Name as ProductName, a.Name as ActionName
	from Plans as p
			left outer join Actions as a on a.ID = p.ActionsID
			left outer join Categories as c1 on c1.ID = p.CategoryID
			left outer join Categories as c2 on c2.ID = p.NecessaryCategoryID
	where p.AgentsID = argAgentID;
END$$

