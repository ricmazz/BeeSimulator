# BeeSimulator

BeeSimulator � uma aplica��o em .NET que permite encontrar palavras v�lidas a partir de uma lista fornecida por URL, com base em uma letra central obrigat�ria e um conjunto de letras permitidas definidas pelo usu�rio.

## Funcionalidades

- Carregamento de uma lista de palavras a partir de uma URL fornecida pelo usu�rio.
- Defini��o din�mica da letra central obrigat�ria.
- Defini��o din�mica das letras permitidas.
- Valida��o e filtragem das palavras com base nos crit�rios fornecidos.
- 
## Sugest�o de lista de palavras v�lidas dispon�veis
- https://www.ime.usp.br/~pf/dicios/br-sem-acentos.txt
- https://raw.githubusercontent.com/pythonprobr/palavras/master/palavras.txt

## Requisitos
- .NET 6 ou superior
- Conex�o � Internet para baixar a lista de palavras

## Como usar

1. Clone este reposit�rio:

    ```bash
    git clone https://github.com/ricmazz/BeeSimulator.git
    cd BeeSimulator
    ```

2. Execute o programa:

    ```bash
    dotnet run
    ```

3. Siga as instru��es no terminal:
    - Insira a URL que cont�m a lista de palavras.
    - Insira a letra central obrigat�ria.
    - Insira as letras permitidas, separadas por espa�o.

## Exemplo de Uso

```plaintext
Insira a URL com as palavras:
https://example.com/palavras.txt
Insira a letra central obrigat�ria:
z
Insira as letras permitidas (separadas por espa�o):
a e i o u t n r s
Possibilidades de palavras v�lidas: 3
arroz
raz�o
zona
```

## Estrutura do Projeto

- `Program.cs`: Cont�m o ponto de entrada do programa e a l�gica principal.
- `IWordLoader`: Interface para carregar palavras de diferentes fontes.
- `UrlWordLoader`: Implementa��o de `IWordLoader` que carrega palavras de uma URL.
- `IWordValidator`: Interface para validar palavras com base em crit�rios definidos.
- `WordValidator`: Implementa��o de `IWordValidator` que valida palavras com base na letra central e nas letras permitidas.

## Contribui��o

Contribui��es s�o bem-vindas! Sinta-se � vontade para abrir uma issue ou enviar um pull request.

## Licen�a

Este projeto est� licenciado sob a licen�a MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## Autor

- [Ricardo Mazzarioli](https://github.com/ricmazz)