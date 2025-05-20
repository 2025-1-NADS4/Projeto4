# app/main.py

from fastapi import FastAPI
from pydantic import BaseModel
from typing import List, Optional
from geopy.distance import geodesic
import pandas as pd
import numpy as np
import joblib
import os

# Criar instância do FastAPI
app = FastAPI()

# Carregar todos os modelos e features
modelos = {}
features = {}

# Pastas
MODELOS_PATH = "app/modelos/"

# Lista dos serviços disponíveis
servicos = ['uberx', '99pop', 'taxi', 'comfort', 'black', 'flash']

# Carregar modelos e features na inicialização
for servico in servicos:
    modelos[servico] = joblib.load(os.path.join(MODELOS_PATH, f"{servico}_modelo.pkl"))
    features[servico] = joblib.load(os.path.join(MODELOS_PATH, f"{servico}_modelo_features.pkl"))

# Definir o que a API espera receber
class PrevisaoRequest(BaseModel):
    lat_origem: float
    lng_origem: float
    lat_destino: float
    lng_destino: float
    ano: int
    mes: int
    hora: int
    tipo_dia: str  # "dia_util" ou "fim_de_semana"
    trafego_estimado: str  # "pico" ou "livre"
    Company_services: Optional[List[str]] = None  # Lista de serviços solicitados

# Rota principal
@app.post("/prever_precos")
def prever_precos(dados: PrevisaoRequest):
    # Calcular distância geodésica
    origem = (dados.lat_origem, dados.lng_origem)
    destino = (dados.lat_destino, dados.lng_destino)
    distancia_km = geodesic(origem, destino).km

    # Montar entrada base
    entrada = {
        'ano': dados.ano,
        'mes': dados.mes,
        'hora': dados.hora,
        'distancia_km': distancia_km,
        'tipo_dia': dados.tipo_dia,
        'trafego_estimado': dados.trafego_estimado
    }

    df_entrada = pd.DataFrame([entrada])
    df_entrada = pd.get_dummies(df_entrada)

    # Filtrar serviços solicitados
    servicos_solicitados = (
        [s.lower() for s in dados.Company_services] if dados.Company_services else servicos
    )

    resultados = {}

    for servico in servicos_solicitados:
        if servico not in modelos:
            resultados[servico] = "Serviço não encontrado"
            continue

        modelo = modelos[servico]
        colunas_esperadas = features[servico]

        df_input = df_entrada.copy()

        for col in colunas_esperadas:
            if col not in df_input.columns:
                df_input[col] = 0

        df_input = df_input[colunas_esperadas]

        preco_estimado = modelo.predict(df_input)[0]
        resultados[servico] = round(preco_estimado, 2)

    return {"previsoes": resultados}
