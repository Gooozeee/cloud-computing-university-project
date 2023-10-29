<?php

use PHPUnit\Framework\TestCase;
use GuzzleHttp\Client;
use GuzzleHttp\Exception\ClientException;

class functionsIntegrationTest extends TestCase
{
    // Test the API with valid data
    public function testGetMaxMinIntegration(): void
    {
        $client = new Client();
        $res = $client->request('GET', 'http://qubgrademe-maxmin.40266405.qpc.hal.davecutting.uk/?module1=m1&mark1=70&module2=m2&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
        $json = $res->getBody()->getContents();

        print_r($json); 

        $this->assertContains("\"max_module\":\"m1 - 70\",\"min_module\":\"m4 - 50\"", $json);
        $this->assertTrue(str_contains($json, "false"));
    }

    // Test the API with a missing module
    public function testGetMaxMinIntegrationMissingModule(): void
    {
        $this->expectException(ClientException::class);
        $client = new Client();
        $res = $client->request('GET', 'http://qubgrademe-maxmin.40266405.qpc.hal.davecutting.uk/?module1=m1&mark1=70&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
    }    

    // Test the API with a module that is blank
    public function testGetMaxMinIntegrationBlankModule(): void
    {
        $this->expectException(ClientException::class);
        $client = new Client();
        $res = $client->request('GET', 'http://qubgrademe-maxmin.40266405.qpc.hal.davecutting.uk/?module1=m1&mark1=70&module2=&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
    }   
    
    // Test the API with a missing mark
    public function testGetMaxMinIntegrationMissingMark(): void
    {
        $this->expectException(ClientException::class);
        $client = new Client();
        $res = $client->request('GET', 'http://qubgrademe-maxmin.40266405.qpc.hal.davecutting.uk/?module1=m1&module2=m2&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
    }  

    // Test the API with a blank mark
    public function testGetMaxMinIntegrationBlankMark(): void
    {
        $this->expectException(ClientException::class);
        $client = new Client();
        $res = $client->request('GET', 'http://qubgrademe-maxmin.40266405.qpc.hal.davecutting.uk/?module1=m1&mark1=module2=m2&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
    }  

     // Test the API with a mark above 100
     public function testGetMaxMinIntegrationMarkAbove100(): void
     {
         $this->expectException(ClientException::class);
         $client = new Client();
         $res = $client->request('GET', 'http://qubgrademe-maxmin.40266405.qpc.hal.davecutting.uk/?module1=m1&mark1=123module2=m2&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
     }

     // Test the API with a mark below 0
     public function testGetMaxMinIntegrationMarkBelow0(): void
     {
         $this->expectException(ClientException::class);
         $client = new Client();
         $res = $client->request('GET', 'http://qubgrademe-maxmin.40266405.qpc.hal.davecutting.uk/?module1=m1&mark1=-56module2=m2&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
     }

     // Test the API with a mark as a string
     public function testGetMaxMinIntegrationMarkAsAString(): void
     {
         $this->expectException(ClientException::class);
         $client = new Client();
         $res = $client->request('GET', 'http://qubgrademe-maxmin.40266405.qpc.hal.davecutting.uk/?module1=m1&mark1=admodule2=m2&mark2=60&module3=m3&mark3=70&module4=m4&mark4=50&module5=m5&mark5=70');
     }

}