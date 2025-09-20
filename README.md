# Practica Programada 1

## Juan Pablo Solís Benamburg - Carnet: FH21009671

### Comandos utilizados:
```bash
dotnet new console -n Practica_Programada_1
dotnet run
```
### Prompts
- Puedes crear la funcion para esto: SumIte: retorna igualmente la suma de los primeros números enteros positivos utilizando una versión iterativa equivalente a la siguiente función recursiva: SumRec(1) = 1 SumRec(n) = n + SumRec(n - 1) Es C# .net 8.0, es para un proyecto de consola

### La respuesta a la siguientes preguntas:

- **¿Por qué todos los valores resultantes tanto de n como de sum difieren entre métodos (fórmula e implementación iterativa) y estrategias (ascendente y descendente)?**
  - El principal causante es el Overflow y el como se alcanza, de una manera se llega a dicho punto más rápido y en el otro no, por eso comienzan a diferir
- **¿Qué cree que sucedería si se utilizan las mismas estrategias (ascendente y descendente) pero con el método recursivo de suma (SumRec)? [si desea puede implementarlo y observar qué sucede en ambos escenarios]**
  - La pila de llamadas se llena de manera mas rapida, en la descendente por ejemplo es casi inmediata, esto causa un stackOverflow en ambas (ascendente y descendente)
