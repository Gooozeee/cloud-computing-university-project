package com.averagegrade.qubgrademeaveragegrade;

import static org.junit.jupiter.api.Assertions.assertEquals;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

import org.junit.jupiter.api.Test;

import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.web.client.HttpClientErrorException;

@SpringBootTest
public class QubGrademeAverageIntegrationTest {

    // Testing the average grade with 70
    @Test
	public void shouldReturnAverageGrade() throws Exception {
        String GET_URL = "http://qubgrademe-averagegrade.40266405.qpc.hal.davecutting.uk/averagegrade?mark1=70&mark2=70&mark3=70&mark4=70&mark5=70";
		URL obj = new URL(GET_URL);
		HttpURLConnection con = (HttpURLConnection) obj.openConnection();
		con.setRequestMethod("GET");
		int responseCode = con.getResponseCode();

		BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
		String inputLine;
		StringBuffer response = new StringBuffer();

		while ((inputLine = in.readLine()) != null) {
			response.append(inputLine);
		}
		in.close();

		assertEquals(responseCode, 200);
		assertEquals(response.toString(), "{\"Message\": \"Your average grade is: 70\"}");
	}

    // Testing the average grade with 53
    @Test
	public void shouldReturnAverageGrade53() throws Exception {
        String GET_URL = "http://qubgrademe-averagegrade.40266405.qpc.hal.davecutting.uk/averagegrade?mark1=53&mark2=53&mark3=53&mark4=53&mark5=53";
		URL obj = new URL(GET_URL);
		HttpURLConnection con = (HttpURLConnection) obj.openConnection();
		con.setRequestMethod("GET");
		int responseCode = con.getResponseCode();

		BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
		String inputLine;
		StringBuffer response = new StringBuffer();

		while ((inputLine = in.readLine()) != null) {
			response.append(inputLine);
		}
		in.close();

		assertEquals(responseCode, 200);
		assertEquals(response.toString(), "{\"Message\": \"Your average grade is: 53\"}");
	}

    // Testing the average grade with a missing mark
	@Test
	public void shouldReturnAverageGradeMarkMissing() throws Exception {
		try {
			String GET_URL = "http://qubgrademe-averagegrade.40266405.qpc.hal.davecutting.uk/averagegrade?mark1=70&mark3=70&mark4=70&mark5=70";
			URL obj = new URL(GET_URL);
			HttpURLConnection con = (HttpURLConnection) obj.openConnection();
			con.setRequestMethod("GET");
			int responseCode = con.getResponseCode();

			BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
			String inputLine;
			StringBuffer response = new StringBuffer();

			while ((inputLine = in.readLine()) != null) {
				response.append(inputLine);
			}
			in.close();

        	System.out.print("Response on new line");
        	System.out.print(response.toString());
		} catch(Exception e)
		{
			assertEquals("java.io.IOException: Server returned HTTP response code: 400 for URL: http://qubgrademe-averagegrade.40266405.qpc.hal.davecutting.uk/averagegrade?mark1=70&mark3=70&mark4=70&mark5=70"
			, e.toString());
			System.out.println("Exception string" + e.toString());
		}
	}

	// Testing the average grade with a mark as a string
	@Test
	public void shouldReturnAverageGradeMarkasAString() throws Exception {
		try {
			String GET_URL = "http://qubgrademe-averagegrade.40266405.qpc.hal.davecutting.uk/averagegrade?mark1=ads&mark2=70&mark3=70&mark4=70&mark5=70";
			URL obj = new URL(GET_URL);
			HttpURLConnection con = (HttpURLConnection) obj.openConnection();
			con.setRequestMethod("GET");
			int responseCode = con.getResponseCode();

			BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
			String inputLine;
			StringBuffer response = new StringBuffer();

			while ((inputLine = in.readLine()) != null) {
				response.append(inputLine);
			}
			in.close();

        	System.out.print("Response on new line");
        	System.out.print(response.toString());
		} catch(Exception e)
		{
			assertEquals("java.io.IOException: Server returned HTTP response code: 400 for URL: http://qubgrademe-averagegrade.40266405.qpc.hal.davecutting.uk/averagegrade?mark1=ads&mark2=70&mark3=70&mark4=70&mark5=70", e.toString());
			System.out.println("Exception string" + e.toString());
		}
	}

	// Testing the average grade with a mark as a blank string
	@Test
	public void shouldReturnAverageGradeMarkasABlankString() throws Exception {
		try {
			String GET_URL = "http://qubgrademe-averagegrade.40266405.qpc.hal.davecutting.uk/averagegrade?mark1=&mark2=70&mark3=70&mark4=70&mark5=70";
			URL obj = new URL(GET_URL);
			HttpURLConnection con = (HttpURLConnection) obj.openConnection();
			con.setRequestMethod("GET");
			int responseCode = con.getResponseCode();

			BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
			String inputLine;
			StringBuffer response = new StringBuffer();

			while ((inputLine = in.readLine()) != null) {
				response.append(inputLine);
			}
			in.close();

        	System.out.print("Response on new line");
        	System.out.print(response.toString());
		} catch(Exception e)
		{
			assertEquals("java.io.IOException: Server returned HTTP response code: 400 for URL: http://qubgrademe-averagegrade.40266405.qpc.hal.davecutting.uk/averagegrade?mark1=&mark2=70&mark3=70&mark4=70&mark5=70", e.toString());
			System.out.println("Exception string" + e.toString());
		}
	}

	// Testing the average grade with a mark below 0
	@Test
	public void shouldReturnAverageGradeMarkBelowZero() throws Exception {
		try {
			String GET_URL = "http://qubgrademe-averagegrade.40266405.qpc.hal.davecutting.uk/averagegrade?mark1=-54&mark2=70&mark3=70&mark4=70&mark5=70";
			URL obj = new URL(GET_URL);
			HttpURLConnection con = (HttpURLConnection) obj.openConnection();
			con.setRequestMethod("GET");
			int responseCode = con.getResponseCode();

			BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
			String inputLine;
			StringBuffer response = new StringBuffer();

			while ((inputLine = in.readLine()) != null) {
				response.append(inputLine);
			}
			in.close();

        	System.out.print("Response on new line");
        	System.out.print(response.toString());
		} catch(Exception e)
		{
			assertEquals("java.io.IOException: Server returned HTTP response code: 400 for URL: http://qubgrademe-averagegrade.40266405.qpc.hal.davecutting.uk/averagegrade?mark1=-54&mark2=70&mark3=70&mark4=70&mark5=70", e.toString());
			System.out.println("Exception string" + e.toString());
		}
	}

	// Testing the average grade with a mark above 100
	@Test
	public void shouldReturnAverageGradeMarkAboveOneHundred() throws Exception {
		try {
			String GET_URL = "http://qubgrademe-averagegrade.40266405.qpc.hal.davecutting.uk/averagegrade?mark1=157&mark2=70&mark3=70&mark4=70&mark5=70";
			URL obj = new URL(GET_URL);
			HttpURLConnection con = (HttpURLConnection) obj.openConnection();
			con.setRequestMethod("GET");
			int responseCode = con.getResponseCode();

			BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
			String inputLine;
			StringBuffer response = new StringBuffer();

			while ((inputLine = in.readLine()) != null) {
				response.append(inputLine);
			}
			in.close();

        	System.out.print("Response on new line");
        	System.out.print(response.toString());
		} catch(Exception e)
		{
			assertEquals("java.io.IOException: Server returned HTTP response code: 400 for URL: http://qubgrademe-averagegrade.40266405.qpc.hal.davecutting.uk/averagegrade?mark1=157&mark2=70&mark3=70&mark4=70&mark5=70", e.toString());
			System.out.println("Exception string" + e.toString());
		}
	}
}