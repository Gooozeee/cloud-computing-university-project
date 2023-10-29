package com.averagegrade.qubgrademeaveragegrade;

import static org.hamcrest.Matchers.containsString;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import org.junit.jupiter.api.Test;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.web.servlet.MockMvc;

@SpringBootTest
@AutoConfigureMockMvc
public class QubgrademeAverageGradeMockTest {

	@Autowired
	private MockMvc mockMvc;

	// Testing the average grade method with valid data
	@Test
	public void shouldCalculateAverageGrade() throws Exception {
		this.mockMvc.perform(get("/averagegrade?mark1=75&mark2=75&mark3=75&mark4=75&mark5=75")).andExpect(status().isOk())
				.andExpect(content().string(containsString("Your average grade is: 75")));
	}

	// Testing the average grade method with marks over 100
	@Test
	public void shouldCalculateAverageGrademark1Over100() throws Exception {
		this.mockMvc.perform(get("/averagegrade?mark1=156&mark2=75&mark3=75&mark4=75&mark5=75")).andExpect(status().isBadRequest())
				.andExpect(content().string(containsString("Module 1 is invalid, has to be greater than zero and less than 100")));
	}

	// Testing the average grade method with marks over 100
	@Test
	public void shouldCalculateAverageGrademark2Over100() throws Exception {
		this.mockMvc.perform(get("/averagegrade?mark1=75&mark2=156&mark3=75&mark4=75&mark5=75")).andExpect(status().isBadRequest())
				.andExpect(content().string(containsString("Module 2 is invalid, has to be greater than zero and less than 100")));
	}

	// Testing the average grade method with marks under 0
	@Test
	public void shouldCalculateAverageGrademark2Under0() throws Exception {
		this.mockMvc.perform(get("/averagegrade?mark1=75&mark2=-75&mark3=75&mark4=75&mark5=75")).andExpect(status().isBadRequest())
				.andExpect(content().string(containsString("Module 2 is invalid, has to be greater than zero and less than 100")));
	}

	// Testing the average grade method with marks under 0
	@Test
	public void shouldCalculateAverageGrademark1Under0() throws Exception {
		this.mockMvc.perform(get("/averagegrade?mark1=25&mark2=-75&mark3=75&mark4=75&mark5=75")).andExpect(status().isBadRequest())
				.andExpect(content().string(containsString("Module 2 is invalid, has to be greater than zero and less than 100")));
	}

	// Testing the average grade method with a parameter as a string
	@Test
	public void shouldCalculateAverageGrademark1AsAString() throws Exception {
		this.mockMvc.perform(get("/averagegrade?mark1=jd&mark2=75&mark3=75&mark4=75&mark5=75")).andExpect(status().isBadRequest())
				.andExpect(content().string(containsString("One of the modules is invalid, try making it an integer")));
	}

	// Testing the average grade method with a parameter as a string
	@Test
	public void shouldCalculateAverageGrademark4AsAString() throws Exception {
		this.mockMvc.perform(get("/averagegrade?mark1=78&mark2=75&mark3=75&mark4=asdf&mark5=75")).andExpect(status().isBadRequest())
				.andExpect(content().string(containsString("One of the modules is invalid, try making it an integer")));
	}

	// Testing the average grade method with module 1 missing
	public void shouldCalculateAverageGrademark1Missing() throws Exception {
		this.mockMvc.perform(get("/averagegrade?&mark2=75&mark3=75&mark4=asdf&mark5=75")).andExpect(status().isBadRequest())
				.andExpect(content().string(containsString("mark1 parameter is missing")));
	}

	// Testing the average grade method with module 2 missing
	public void shouldCalculateAverageGrademark2Missing() throws Exception {
		this.mockMvc.perform(get("/averagegrade?&mark1=75&mark3=75&mark4=asdf&mark5=75")).andExpect(status().isBadRequest())
				.andExpect(content().string(containsString("mark1 parameter is missing")));
	}

	// Testing the average grade method with module 3 missing
	public void shouldCalculateAverageGrademark3Missing() throws Exception {
		this.mockMvc.perform(get("/averagegrade?&mark1=75&mark2=75&mark4=asdf&mark5=75")).andExpect(status().isBadRequest())
				.andExpect(content().string(containsString("mark1 parameter is missing")));
	}

	// Testing the average grade method with module 4 missing
	public void shouldCalculateAverageGrademark4Missing() throws Exception {
		this.mockMvc.perform(get("/averagegrade?&mark1=75&mark2=75&mark3=asdf&mark5=75")).andExpect(status().isBadRequest())
				.andExpect(content().string(containsString("mark1 parameter is missing")));
	}

	// Testing the average grade method with module 5 missing
	public void shouldCalculateAverageGrademark5Missing() throws Exception {
		this.mockMvc.perform(get("/averagegrade?&mark1=75&mark2=75&mark3=asdf&mark4=75")).andExpect(status().isBadRequest())
				.andExpect(content().string(containsString("mark1 parameter is missing")));
	}
}