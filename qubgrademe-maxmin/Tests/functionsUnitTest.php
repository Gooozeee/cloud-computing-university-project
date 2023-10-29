<?php

use PHPUnit\Framework\TestCase;
require dirname(dirname(__FILE__)).'/src/functions.inc.php';

class functionsUnitTest extends TestCase
{
    // Test max min with valid data
    public function testGetMaxMin(): void
    {
        $marks = array(70, 60, 70, 50, 70);
        $modules = array("m1", "m2", "m3", "m4", "m5");

        $this->assertContains('m1 - 70', getMaxMin($modules, $marks));
        $this->assertContains('m4 - 50', getMaxMin($modules, $marks));
    }

    // Test check input with valid data
    public function testGetMaxMinValidData(): void
    {
        $marks = array("70", "60", "70", "50", "70");
        $modules = array("m1", "m2", "m3", "m4", "m5");

        $this->assertContains('m1 - 70', getMaxMin($modules, $marks));
        $this->assertContains('m4 - 50', getMaxMin($modules, $marks));
    }

    // Test check input with missing module 1
    public function testGetMaxMinMissingModule1(): void
    {
        $marks = array("70", "60", "70", "50", "70");
        $modules = array("m2", "m3", "m4", "m5");

        $this->assertEquals(checkInput($marks, $modules), false);
    }

    // Test check input with missing module 2
    public function testGetMaxMinMissingModule2(): void
    {
        $marks = array("70", "60", "70", "50", "70");
        $modules = array("m1", "m3", "m4", "m5");

        $this->assertEquals(checkInput($marks, $modules), false);
    }

    // Test check input with missing module 3
    public function testGetMaxMinMissingModule3(): void
    {
        $marks = array("70", "60", "70", "50", "70");
        $modules = array("m1", "m2", "m4", "m5");

        $this->assertEquals(checkInput($marks, $modules), false);
    }

    // Test check input with missing module 4
    public function testGetMaxMinMissingModule4(): void
    {
        $marks = array("70", "60", "70", "50", "70");
        $modules = array("m1", "m2", "m3", "m5");

        $this->assertEquals(checkInput($marks, $modules), false);
    }

    // Test check input with missing module 5
    public function testGetMaxMinMissingModule5(): void
    {
        $marks = array("70", "60", "70", "50", "70");
        $modules = array("m1", "m2", "m3", "m4");

        $this->assertEquals(checkInput($marks, $modules), false);
    }

    // Test check input with missing mark
    public function testGetMaxMinMissingMark(): void
    {
        $marks = array("70", "60", "70", "50");
        $modules = array("m1", "m2", "m3", "m4", "m5");

        $this->assertEquals(checkInput($marks, $modules), false);
    }

    // Test check input with mark as a string
    public function testGetMaxMinInvalidMarkAsString(): void
    {
        $marks = array("70", "60", "70", "50", "alk");
        $modules = array("m1", "m2", "m3", "m4", "m5");

        $this->assertEquals(checkInput($marks, $modules), false);
    }

    // Test check input with mark below 0
    public function testGetMaxMinInvalidMarkBelow0(): void
    {
        $marks = array("70", "60", "70", "-10", "70");
        $modules = array("m1", "m2", "m3", "m4", "m5");

        $this->assertEquals(checkInput($marks, $modules), false);
    }

    // Test check input with mark above 100
    public function testGetMaxMinInvalidMarkAbove100(): void
    {
        $marks = array("70", "60", "70", "60", "153");
        $modules = array("m1", "m2", "m3", "m4", "m5");

        $this->assertEquals(checkInput($marks, $modules), false);
    }
}