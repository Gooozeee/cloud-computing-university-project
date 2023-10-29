<?php

use PHPUnit\Framework\TestCase;
require dirname(dirname(__FILE__)).'/src/functions.inc.php';

class functionsUnitTest extends TestCase
{
    // Test max min with valid data
    public function testSortModules(): void
    {
        $marks = array(70, 60, 70, 50, 70);
        $modules = array("m1", "m2", "m3", "m4", "m5");

        $firstCheck = array("module"=>"m1", "marks"=>"70");
        $secondCheck = array("module"=>"m3", "marks"=>"70");
        $thirdCheck = array("module"=>"m5", "marks"=>"70");
        $fourthCheck = array("module"=>"m2", "marks"=>"60");
        $fifthCheck = array("module"=>"m4", "marks"=>"50"); 

        $this->assertContains($firstCheck, getSortedModules($modules, $marks)[0]);
        $this->assertContains($secondCheck, getSortedModules($modules, $marks)[1]);
        $this->assertContains($thirdCheck, getSortedModules($modules, $marks)[2]);
        $this->assertContains($fourthCheck, getSortedModules($modules, $marks)[3]);
        $this->assertContains($fifthCheck, getSortedModules($modules, $marks)[4]);
    }

    // Test check input with missing module 1
    public function testCheckInputMissingModule1(): void
    {
        $marks = array("70", "60", "70", "50", "70");
        $modules = array("m2", "m3", "m4", "m5");

        $this->assertEquals(checkInput($marks, $modules),false);
    }

    // Test check input with missing module 2
    public function testCheckInputMissingModule2(): void
    {
        $marks = array("70", "60", "70", "50", "70");
        $modules = array("m1", "m3", "m4", "m5");

        $this->assertEquals(checkInput($marks, $modules),false);
    }

    // Test check input with missing module 3
    public function testCheckInputMissingModule3(): void
    {
        $marks = array("70", "60", "70", "50", "70");
        $modules = array("m1", "m2", "m4", "m5");

        $this->assertEquals(checkInput($marks, $modules),false);
    }

    // Test check input with missing module 4
    public function testCheckInputMissingModule4(): void
    {
        $marks = array("70", "60", "70", "50", "70");
        $modules = array("m1", "m2", "m3", "m5");

        $this->assertEquals(checkInput($marks, $modules),false);
    }

    // Test check input with missing module 5
    public function testCheckInputMissingModule5(): void
    {
        $marks = array("70", "60", "70", "50", "70");
        $modules = array("m1", "m2", "m3", "m4");

        $this->assertEquals(checkInput($marks, $modules),false);
    }

    // Test check input with missing mark
    public function testCheckInputMissingMark(): void
    {
        $marks = array("70", "60", "70", "50");
        $modules = array("m1", "m2", "m3", "m4", "m5");

        $this->assertEquals(checkInput($marks, $modules),false);
    }

    // Test check input with mark as a string
    public function testCheckInputInvalidMarkAsString(): void
    {
        $marks = array("70", "60", "70", "50", "alk");
        $modules = array("m1", "m2", "m3", "m4", "m5");

        $this->assertEquals(checkInput($marks, $modules),false);
    }

    // Test check input with mark below 0
    public function testCheckInputInvalidMarkBelow0(): void
    {
        $marks = array("70", "60", "70", "-10", "70");
        $modules = array("m1", "m2", "m3", "m4", "m5");

        $this->assertEquals(checkInput($marks, $modules),false);
    }

    // Test check input with mark above 100
    public function testCheckInputInvalidMarkAbove100(): void
    {
        $marks = array("70", "60", "70", "60", "153");
        $modules = array("m1", "m2", "m3", "m4", "m5");

        $this->assertEquals(checkInput($marks, $modules),false);
    }
}