<?php

use PHPUnit\Framework\TestCase;
use GuzzleHttp\Client;
use GuzzleHttp\Exception\ClientException;

class functionsIntegrationTest extends TestCase
{
    // Test the API with valid data
    public function testSortModulesIntegration(): void
    {
        $client = new Client();
        $res = $client->request('GET', 'http://qubgrademe-sortmodules.40266405.qpc.hal.davecutting.uk/?module1=m1&mark1=70&module2=m2&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
        $json = $res->getBody()->getContents();
        $this->assertTrue(str_contains($json, "false"));
        $responseCheck = "\"sorted_modules\":[{\"module\":\"m1\",\"marks\":\"70\"},{\"module\":\"m3\",\"marks\":\"70\"},{\"module\":\"m5\",\"marks\":\"70\"},{\"module\":\"m2\",\"marks\":\"60\"},{\"module\":\"m4\",\"marks\":\"50\"}]}";
        $this->assertTrue(str_contains($json, $responseCheck));
    }

    // Test the API with a missing module
    public function testSortModulesIntegrationMissingModule(): void
    {
        $this->expectException(ClientException::class);
        $client = new Client();
        $res = $client->request('GET', 'http://qubgrademe-sortmodules.40266405.qpc.hal.davecutting.uk/?module1=m1&mark1=70&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
    }    

    // Test the API with a module that is blank
    public function testSortModulesIntegrationBlankModule(): void
    {
        $this->expectException(ClientException::class);
        $client = new Client();
        $res = $client->request('GET', 'http://qubgrademe-sortmodules.40266405.qpc.hal.davecutting.uk/?module1=m1&mark1=70&module2=&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
    }   
    
    // Test the API with a missing mark
    public function testSortModulesIntegrationMissingMark(): void
    {
        $this->expectException(ClientException::class);
        $client = new Client();
        $res = $client->request('GET', 'http://qubgrademe-sortmodules.40266405.qpc.hal.davecutting.uk/?module1=m1&module2=m2&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
    }  

    // Test the API with a blank mark
    public function testSortModulesIntegrationBlankMark(): void
    {
        $this->expectException(ClientException::class);
        $client = new Client();
        $res = $client->request('GET', 'http://qubgrademe-sortmodules.40266405.qpc.hal.davecutting.uk/?module1=m1&mark1=module2=m2&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
    }  

     // Test the API with a mark above 100
     public function testSortModulesIntegrationMarkAbove100(): void
     {
         $this->expectException(ClientException::class);
         $client = new Client();
         $res = $client->request('GET', 'http://qubgrademe-sortmodules.40266405.qpc.hal.davecutting.uk/?module1=m1&mark1=123module2=m2&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
     }

     // Test the API with a mark below 0
     public function testSortModulesIntegrationMarkBelow0(): void
     {
         $this->expectException(ClientException::class);
         $client = new Client();
         $res = $client->request('GET', 'http://qubgrademe-sortmodules.40266405.qpc.hal.davecutting.uk/?module1=m1&mark1=-56module2=m2&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
     }

     // Test the API with a mark as a string
     public function testSortModulesIntegrationMarkAsAString(): void
     {
         $this->expectException(ClientException::class);
         $client = new Client();
         $res = $client->request('GET', 'http://qubgrademe-sortmodules.40266405.qpc.hal.davecutting.uk/?module1=m1&mark1=admodule2=m2&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
     }

}