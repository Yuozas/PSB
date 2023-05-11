# PSB
Basic plussed string builder (Console Application)

How it works?
---
1. You start the console application.
2. Press y or Y to notify you're ready for the processing.
3. Your string is being taken from clipboard and processed.
4. You get back the new result into the clipboard.
5. Application restarts.

Why to use it?
---
In many languages multiline strings are a bit weird. 
In C# case many would use Join or Verbatim text.
I don't like Verbatim cause it destroys hierarchy view in the code.
As for the `string.Join` aproach it looks a bit overkill even if it allows me to forget about writing `'\n'` in every single line.

Example use case
---
Here let's say we have this sql and we want to use it in our code:
```
SELECT `something`.`id`, `something`.`parent_id`,
JSON_ARRAYAGG(
    JSON_OBJECT(
    	'languageId', `something`.`language_id`,
        'value', `something`.`name`
    )
) AS `translations`,
JSON_ARRAYAGG(
	`something_else`.`id`
) AS `something_else_ids`,
JSON_ARRAYAGG(
	`something_even_more_else`.`id`
) AS `something_even_more_else_ids`

FROM `something` 

LEFT JOIN `something_else`
ON `something_else`.`something_id` = `something`.`id`

LEFT JOIN `something_even_more_else`
ON `something_even_more_else`.`something_id` = `something`.`id`

WHERE `something`.`parent_id` = 1

GROUP BY `something`.`id`
```
This is how the result will look after processing.
```
const string text = "SELECT `something`.`id`, `something`.`parent_id`,\n" + 
"JSON_ARRAYAGG(\n" + 
"    JSON_OBJECT(\n" + 
"    	'languageId', `something`.`language_id`,\n" + 
"        'value', `something`.`name`\n" + 
"    )\n" + 
") AS `translations`,\n" + 
"JSON_ARRAYAGG(\n" + 
"	`something_else`.`id`\n" + 
") AS `something_else_ids`,\n" + 
"JSON_ARRAYAGG(\n" + 
"	`something_even_more_else`.`id`\n" + 
") AS `something_even_more_else_ids`\n" + 
"\n" + 
"FROM `something` \n" + 
"\n" + 
"LEFT JOIN `something_else`\n" + 
"ON `something_else`.`something_id` = `something`.`id`\n" + 
"\n" + 
"LEFT JOIN `something_even_more_else`\n" + 
"ON `something_even_more_else`.`something_id` = `something`.`id`\n" + 
"\n" + 
"WHERE `something`.`parent_id` = 1\n" + 
"\n" + 
"GROUP BY `something`.`id`\n";
```
