<?php
function getMaxMin($modules, $marks)
{
    $module_marks = array();
    for ($i = 0; $i < count($modules); $i++) {
      $module_marks_array = array("module"=>$modules[$i], "marks"=>$marks[$i]);
      array_push($module_marks,$module_marks_array);
    }

    usort($module_marks, function($a, $b) {
          return $b['marks'] <=> $a['marks'];
    });

    $max_module = $module_marks[0]['module'] . ' - ' . $module_marks[0]['marks'];
    $min_module = $module_marks[count($module_marks)-1]['module'] . ' - ' . $module_marks[count($module_marks)-1]['marks'];

    return array($max_module,$min_module);
}

function checkInput($modules, $marks)
{
  for($i = 0; $i < count($marks); $i++)
{
	// Check if marks are null
	if(is_null($marks[$i])) {
		http_response_code(400);
		$output = array(
			"error" => true,
			"Message" => "A mark can't be null"
		);
		echo json_encode($output);
		exit();
	}
	// Check if mark is not an integer
	$int_value = ctype_digit($marks[$i]) ? intval($marks[$i]) : null;
	if($int_value === null) {
		http_response_code(400);
		$output = array(
			"error" => true,
			"Message" => "A mark has to be an integer"
		);
		echo json_encode($output);
		exit();
	}
	// Check if marks are greater than 0 and less than 100
	if($marks[$i] < 0 || $marks[$i] > 100) {
		http_response_code(400);
		$output = array(
			"error" => true,
			"Message" => "A mark has to be between 0 and 100"
		);
		echo json_encode($output);
		exit();
	}
}

for($i = 0; $i < count($modules); $i++)
{
	// Check if modules are null
	if(is_null($modules[$i])) {
		http_response_code(400);
		$output = array(
			"error" => true,
			"Message" => "A module can't be null"
		);
		echo json_encode($output);
		exit();
	}
	if($modules[$i] == "") {
		http_response_code(400);
		$output = array(
			"error" => true,
			"Message" => "A module can't be blank"
		);
		echo json_encode($output);
		exit();
	}
}
}
