
# FECAP - Fundação de Comércio Álvares Penteado

<p align="center">
<a href= "https://www.fecap.br/"><img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRhZPrRa89Kma0ZZogxm0pi-tCn_TLKeHGVxywp-LXAFGR3B1DPouAJYHgKZGV0XTEf4AE&usqp=CAU" alt="FECAP - Fundação de Comércio Álvares Penteado" border="0"></a>
</p>

# Nome do Projeto

## Nome do Grupo

## Integrantes: <a href="https://www.linkedin.com/in/giovanne-braga-0a4288280/">Giovanne Braga</a>, <a href="https://www.linkedin.com/in/isaac-fs-santos/">Isaac Santos</a>, <a href="https://www.linkedin.com/in/caroline-gomes-446882230/">Caroline Gomes</a>, <a href="https://www.linkedin.com/in/icaro-dellalo/">Icaro Luis Dellalo</a>

## Professores Orientadores: <a href="https://www.linkedin.com/in/ronaldo-araujo-pinto-3542811a/">Ronaldo Araújo</a>, <a href="https://www.linkedin.com/in/eduardo-savino-gomes-77833a10/">Eduardo Savino</a>, <a href="https://www.linkedin.com/in/lucymari/?originalSubdomain=br">Lucy Mari</a>, <a href="https://www.linkedin.com/in/edsonbarbero/">Edson Barbero</a>, <a href="https://www.linkedin.com/in/aimarlopes/">Aimar Lopes</a>

## Descrição

<p align="center">
<img src="https://github.com/2025-1-NADS4/Projeto4/blob/main/imagens/cover.png" alt="CAPA FASOR" border="0">
</p>

FASOR - Fare Advisor é um aplicativo que auxilia usuários de transporte por aplicativo (como Uber e 99) a comparar em tempo real as tarifas praticadas por diferentes serviços.
Para isso, foi criado e treinado um modelo de machine learning com base em uma base de dados real fornecida, capaz de prever os valores médios das tarifas em determinadas condições. Com essa previsão, o app exibe ao usuário as opções conforme local de partida, destino, horário e demanda.<br>
Esse desafio foi proposto pela FECAP como entrega do Projeto Integrador de Startup Digital

## 🛠 Estrutura de pastas

-Raiz<br>
|<br>
|-->documentos<br>
  &emsp;|-->Entrega 1<br>
  &emsp;|-->Entrega 2<br>
  &emsp;|-->Entrega 3<br>
  &emsp;|-->Outros documentos<br>

|-->imagens<br>
|-->src<br>
  &emsp;|-->Backend<br>
  &emsp;|-->Frontend<br>
  &emsp;&emsp;|-->FasorAdmin<br>
  &emsp;&emsp;|-->FasorMobile<br>
|readme.md<br>

A pasta raiz contem dois arquivos que devem ser alterados:

<b>README.MD</b>: Arquivo que serve como guia e explicação geral sobre seu projeto. O mesmo que você está lendo agora.

Há também 4 pastas que seguem da seguinte forma:

<b>documentos</b>: Toda a documentação estará nesta pasta.

<b>executáveis</b>: Binários e executáveis do projeto devem estar nesta pasta.

<b>imagens</b>: Imagens do sistema

<b>src</b>: Pasta que contém o código fonte.


## 💻 Configuração para Desenvolvimento

Para rodar a aplicação Mobile apenas clone o repositório de Front-end e rode o comando abaixo descrito em **Mobile**, para rodar a API que traz a cotação, clone o repositório pertinente a IA no PyCharm (recomendado) e rode os comandos descritos em IA;   

Para abrir este projeto você necessita das seguintes ferramentas:

-<a href="https://www.jetbrains.com/pt-br/pycharm/download/?section=windows">PyCharm</a><br>
-<a href="https://visualstudio.microsoft.com/pt-br/downloads/">Visual Studio</a><br>
-<a href="https://code.visualstudio.com/download">Visual Studio Code</a>

Mobile:

Instale o Expo Go na PlayStore
Escaneie o QR Code após o "npm start"

```sh
npm install
npm start
```

IA:

```sh
pip install -r requirements.txt

uvicorn app.main:app --reload

```
Acesse o IP indicado incluindo o /docs

## 📋 Licença/License
<p xmlns:cc="http://creativecommons.org/ns#" xmlns:dct="http://purl.org/dc/terms/"><a property="dct:title" rel="cc:attributionURL" href="https://github.com/2025-1-NADS4/Projeto4/">FASOR</a> by <a rel="cc:attributionURL dct:creator" property="cc:attributionName" <a href="https://www.linkedin.com/in/giovanne-braga-0a4288280/">Giovanne Braga</a>, <a href="https://www.linkedin.com/in/isaac-fs-santos/">Isaac Santos</a>, <a href="https://www.linkedin.com/in/icaro-dellalo/">Icaro Dellalo</a>, <a href="https://www.linkedin.com/in/caroline-gomes-446882230/">Caroline Gomes</a> is licensed under <a href="https://creativecommons.org/licenses/by/4.0/?ref=chooser-v1" target="_blank" rel="license noopener noreferrer" style="display:inline-block;">CC BY 4.0<img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/cc.svg?ref=chooser-v1" alt=""><img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/by.svg?ref=chooser-v1" alt=""></a></p>

## 🎓 Referências

Aqui estão as referências usadas no projeto.

1. <https://docs.expo.dev/>
2. <https://reactnative.dev/docs/getting-started/>
3. <https://learn.microsoft.com/en-us/dotnet/framework/>

