public class TestResult
{
    public string nome { get; set; }
    public string nascimento { get; set; }
    public Prova prova1 { get; set; }
    public Prova prova2 { get; set; }
}

public class Prova
{
    public int triangulo { get; set; }
    public int quadrado { get; set; }
    public int circulo { get; set; }
    public int estrela { get; set; }
    public int hexagono { get; set; }
    public int tempo { get; set; }
    public int quantidade { get; set; }
    public double acertos { get; set; }
}