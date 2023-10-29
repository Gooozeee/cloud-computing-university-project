package com.averagegrade.qubgrademeaveragegrade;

import static org.junit.jupiter.api.Assertions.assertEquals;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

import org.junit.jupiter.api.Test;

import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;

@SpringBootTest
@AutoConfigureMockMvc
public class QubGradeMeIndexIntegrationTest {
    @Test
	public void shouldReturnDefaultMessageForIndex() throws Exception {
        String GET_URL = "http://qubgrademe-averagegrade.40266405.qpc.hal.davecutting.uk/";
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
		assertEquals(response.toString(), "{\"Message\": \"Welcome to the QUB Grade Me Average Grade API \"}");
	}
}
