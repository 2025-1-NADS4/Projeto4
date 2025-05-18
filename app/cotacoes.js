import { useRouter } from 'expo-router';
import React, { useState, useEffect, useRef } from 'react';
import { View, StyleSheet, SafeAreaView, Alert } from 'react-native';
import MapView, { PROVIDER_GOOGLE } from 'react-native-maps';
import { FlatList, ActivityIndicator } from 'react-native';
import * as Location from 'expo-location';
import HeaderIndex from '../components/headerIndex';
import TabBar from '../components/footer';
import CotacaoCard from '../components/cotacaoCard';


export default function CotacoesScreen() {
  const [region, setRegion] = useState(null);
  const mapRef = useRef(null);
  const router = useRouter();
  const [cotacoes, setCotacoes] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    (async () => {
      let { status } = await Location.requestForegroundPermissionsAsync();
      if (status !== 'granted') {
        Alert.alert('Permissão negada', 'Precisamos da localização para continuar.');
        return;
      }

      let location = await Location.getCurrentPositionAsync({});
      setRegion({
        latitude: location.coords.latitude,
        longitude: location.coords.longitude,
        latitudeDelta: 0.01,
        longitudeDelta: 0.01,
      });
    })();
  }, []);

  useEffect(() => {
    const mockData = [
      {
        id: '1',
        logo: require('../assets/logo_99.png'), 
        titulo: '99Pop',
        descricao: '*Esse valor é uma estimativa, valores reais podem não corresponder',
        preco: 'R$ 149,90',
      },
      {
        id: '2',
        logo: require('../assets/logo_uber.png'),
        titulo: 'UberX',
        descricao: '*Esse valor é uma estimativa, valores reais podem não corresponder',
        preco: 'R$ 89,00',
      },
     
    ];

    setTimeout(() => {
      setCotacoes(mockData);
      setLoading(false);
    }, 1500); // Simula carregamento de 1,5s
  }, []);

  if (loading) {
    return (
      <View style={styles.loading}>
        <ActivityIndicator size="large" color="#ED6FA9" />
      </View>
    );
  }

  return (
    <View style={{ flex: 1 }}>
      <SafeAreaView style={styles.container}>
        <View style={{ width: '100%', position: 'relative' }}>
          <HeaderIndex />
        </View>

       {region && (
        <MapView
          ref={mapRef}
          provider={PROVIDER_GOOGLE}
          style={styles.overlayMap}
          region={region}
          showsUserLocation={true}
          zoomEnabled
          scrollEnabled
          rotateEnabled
          pitchEnabled
          showsCompass
        />
      )}

      <Text style={styles.separador}>Resultados</Text>
      
      <FlatList
                data={cotacoes}
                keyExtractor={(item) => item.id}
                renderItem={({ item }) => (
                  <CotacaoCard
                    logo={item.logo}
                    titulo={item.titulo}
                    descricao={item.descricao}
                    preco={item.preco}
                  />
                )}
                contentContainerStyle={{ 
                  paddingTop: 200, // valor maior que a altura do mapa
                  paddingBottom: 100,
                  paddingHorizontal: 16,
                 }} // Espaço para o footer
              />


        <TabBar />
      </SafeAreaView>
       

    </View>
    
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
overlayMap: {
  position: 'absolute',
  top: 20, // distância do topo da tela (ajuste conforme necessário)
  alignSelf: 'center',
  width: '90%',
  height: '35%', // altura próxima à do header (ajuste conforme o tamanho do seu HeaderIndex)
  borderRadius: 12,
  borderWidth: 2,
  borderColor: '#D9D9D9',
  //zIndex: 10,
},
separador: {
    fontSize: 18,
    fontWeight: 'bold',
    marginVertical: 12,
    textAlign: 'center',
    color: '#333',
    
  },
});
