# BeeSimulator

BeeSimulator é uma aplicação em .NET que permite encontrar palavras válidas a partir de uma lista fornecida por URL, com base em uma letra central obrigatória e um conjunto de letras permitidas definidas pelo usuário.

## Funcionalidades

- Carregamento de uma lista de palavras a partir de uma URL fornecida pelo usuário.
- Definição dinâmica da letra central obrigatória.
- Definição dinâmica das letras permitidas.
- Validação e filtragem das palavras com base nos critérios fornecidos.
  
## Sugestão de lista de palavras válidas disponíveis
- https://www.ime.usp.br/~pf/dicios/br-sem-acentos.txt
- https://raw.githubusercontent.com/pythonprobr/palavras/master/palavras.txt

## Requisitos
- .NET 6 ou superior
- Conexão à Internet para baixar a lista de palavras

## Como usar

1. Clone este repositório:

    ```bash
    git clone https://github.com/ricmazz/BeeSimulator.git
    cd BeeSimulator
    ```

2. Execute o programa:

    ```bash
    dotnet run
    ```

3. Siga as instruções no terminal:
    - Insira a URL que contém a lista de palavras.
    - Insira a letra central obrigatória.
    - Insira as letras permitidas, separadas por espaço.

## Exemplo de Uso

```plaintext
Insira a URL com as palavras:
https://example.com/palavras.txt
Insira a letra central obrigatória:
z
Insira as letras permitidas (separadas por espaço):
a e i o u t n r s
Possibilidades de palavras válidas: 3
arroz
razão
zona
```

## Estrutura do Projeto

- `Program.cs`: Contém o ponto de entrada do programa e a lógica principal.
- `IWordLoader`: Interface para carregar palavras de diferentes fontes.
- `UrlWordLoader`: Implementação de `IWordLoader` que carrega palavras de uma URL.
- `IWordValidator`: Interface para validar palavras com base em critérios definidos.
- `WordValidator`: Implementação de `IWordValidator` que valida palavras com base na letra central e nas letras permitidas.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir uma issue ou enviar um pull request.

## Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## Autor

- [Ricardo Mazzarioli](https://github.com/ricmazz)
