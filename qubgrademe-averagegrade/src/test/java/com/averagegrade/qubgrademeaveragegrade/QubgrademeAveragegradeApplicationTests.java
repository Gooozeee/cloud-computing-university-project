package com.averagegrade.qubgrademeaveragegrade;

import static org.assertj.core.api.Assertions.assertThat;

import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

@SpringBootTest
class QubgrademeAveragegradeApplicationTests {

	@Autowired
	private QubGradeMeAverageGradeController controller;

	@Test
	void contextLoads() {
		assertThat(controller).isNotNull();
	}


}
