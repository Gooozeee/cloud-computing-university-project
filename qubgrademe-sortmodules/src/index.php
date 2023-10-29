<?php
header("Access-Control-Allow-Origin: *");
header("Content-type: application/json");
require('functions.inc.php');



$module_1 = $_REQUEST['module1'];
$module_2 = $_REQUEST['module2'];
$module_3 = $_REQUEST['module3'];
$module_4 = $_REQUEST['module4'];
$module_5 = $_REQUEST['module5'];
$mark_1 = $_REQUEST['mark1'];
$mark_2 = $_REQUEST['mark2'];
$mark_3 = $_REQUEST['mark3'];
$mark_4 = $_REQUEST['mark4'];
$mark_5 = $_REQUEST['mark5'];

$modules = array($module_1,$module_2,$module_3,$module_4,$module_5);
$marks = array($mark_1,$mark_2,$mark_3,$mark_4,$mark_5);

checkInput($modules, $marks);
$sorted_modules=getSortedModules($modules, $marks);


$output = array(
	"error" => false,
  "modules" => "",
	"marks" => 0,
	"sorted_modules" => ""
);

$output['modules']=$modules;
$output['marks']=$marks;
$output['sorted_modules']=$sorted_modules;

echo json_encode($output);
exit();
