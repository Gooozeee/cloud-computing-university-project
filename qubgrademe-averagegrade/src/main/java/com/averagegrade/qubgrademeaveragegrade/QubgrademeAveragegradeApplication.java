package com.averagegrade.qubgrademeaveragegrade;

import java.util.Arrays;

import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.context.ApplicationContext;

@SpringBootApplication
public class QubgrademeAveragegradeApplication {

	public static void main(String[] args) {
		SpringApplication.run(QubgrademeAveragegradeApplication.class, args);
	}

	@Bean
	public CommandLineRunner commandLineRunner(ApplicationContext ctx)
	{
		return args -> {
			//System.out.println("Let's inspect the beans provided by spring boot");

			String[] beanNames = ctx.getBeanDefinitionNames();
			Arrays.sort(beanNames);
			for (String beanName : beanNames)
			{
				//System.out.println(beanName);
			}
		};
	}

}
