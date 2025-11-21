# BeeSimulator

BeeSimulator é uma aplicação console em .NET 6 que ajuda a encontrar palavras válidas a partir de um dicionário online, usando uma letra central obrigatória e um conjunto de letras permitidas.

## Requisitos
- .NET 6 ou superior
- Conexão à Internet para baixar o dicionário escolhido

## Execução rápida
O programa precisa receber um argumento numérico indicando qual fonte de palavras usar:
- `1`: https://www.ime.usp.br/~pf/dicios/br-sem-acentos.txt
- `2`: https://raw.githubusercontent.com/pythonprobr/palavras/master/palavras.txt

```bash
git clone https://github.com/ricmazz/BeeSimulator.git
cd BeeSimulator
dotnet run -- 1
```

> Use `dotnet run -- 2` se preferir a segunda lista.

## Fluxo no terminal
1) O programa baixa a lista de palavras da URL escolhida.  
2) Ele pede:
   - **Letra central**: uma letra obrigatória (qualquer outra letra informada será ignorada).  
   - **Letras permitidas**: digite todas as letras que quer permitir, separadas por espaço (`a e i o u t n r s`).  
3) A lista é filtrada e impressa, mostrando primeiro a quantidade total e depois cada palavra válida.

## Critérios de validação
- Palavras entre 4 e 15 caracteres.
- A palavra precisa conter a letra central.
- Todos os caracteres devem estar no conjunto de letras permitidas (a letra central é adicionada automaticamente a esse conjunto).
- Acentos são removidos antes da validação (ex.: `á`, `ã`, `â` viram `a`).

### Exemplo de sessão
```plaintext
dotnet run -- 1
Insira a letra central obrigatória:
z
Insira as letras permitidas (separadas por espaço):
a e i o u t n r s
Possibilidades de palavras válidas: 3
arroz
razão
zona
```

## Estrutura do projeto
- `Program.cs`: ponto de entrada e orquestração do fluxo.
- `IWordLoader` / `UrlWordLoader`: baixa e carrega as palavras da URL selecionada.
- `IWordValidator` / `WordValidator`: normaliza (remove acentos) e aplica as regras de validação.

## Contribuição
Pull requests e issues são bem-vindos. Sugestões de novas listas de palavras ou melhorias de usabilidade ajudam bastante.

## Licença
Este projeto está licenciado sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## Autor
- [Ricardo Mazzarioli](https://github.com/ricmazz)
