# senactcc-desktop

Projeto Integrador (TCC) do Técnico em Informática.

## Começando
Neste projeto, que seria para conclusão das unidades curriculares de desenvolvimento, nosso grupo teve a oportunidade de ter **exclusivamente** o Senac (São Miguel Paulista) como nosso "cliente". Eles apresentaram um problema, o qual se refere à gestão do aluguel dos equipamentos na instituição. Houve várias reuniões com a área responsável por esta parte, e assim, foi nos dado o desafio de desenvolver um software para que possam fazer esta gestão com excelência e facilidade, ao contrário do que antes era feito: de forma arcaica e desatualizada.

> Por fim, a tecnologia desenvolvida de fato **entrará em vigor** nesta unidade Senac.

### O que encontrarei neste repositório?
Neste repositório se encontra o sistema administrativo desenvolvido em .Net C#, junto à Web API que compartilha da mesma biblioteca.

## Features
- Visualização de agendamentos em andamento e concluídos de todos os docentes, com filtro de pesquisa e detalhes do processo. Além de funcionalidade que permite a exportação dos dados exibidos em um arquivo *.xlsx*.
- Autenticação de login do funcionário com o usuario dele na rede por AD.
- CRUD de equipamentos, número de patrimônio, categorias, funcionários.

## Construído com
- WPF do .NET Framework utilizando C# - Construção da interface gráfica
- Material Design in XAML - Framework de estilização da interface gráfica
- Dapper - Para conexão e Queryes com o banco de dados
- Interop Excel - Para exportação de dados diretamente para arquivo *.xlsx*

## Versionamento
Usamos o Git para controles de versão. Inicialmente o projeto foi estruturado e desenvolvido num [repositório](https://dev.azure.com/teambluescreen/Projeto%20Integrador/_git/Desktop) no Azure DevOps, que posteriormente foi importado aqui.

## Autores
- **Victor Martins Tinoco** - [victrmart](https://github.com/victrmart/)
- **Gabriel Yago Luz** - [gabrielluz23](https://github.com/gabrielluz23/)

Veja também a [lista](https://www.linkedin.com/in/victormartinstinoco/detail/project/924880102/contributors/) de colaboradores que participaram deste projeto.

## Licença
Este projeto está licenciado sob a licença MIT (consulte o arquivo [LICENSE.md](LICENSE.md) para obter mais detalhes).

## Agradecimentos
- Ao Senac pela oportunidade e pela bolsa de estudo.
- Aos professores [Erickson Lima](https://github.com/ericksonlbs) e [Roni Costa](https://www.linkedin.com/in/ronicosta1/) pelo apoio e suporte nas UCs de desenvolvimento.
