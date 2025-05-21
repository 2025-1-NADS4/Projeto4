import { useRouter, useLocalSearchParams } from 'expo-router';
import React, { useState, useEffect, useRef } from 'react';
import { View, StyleSheet, SafeAreaView, Alert, Text } from 'react-native';
import MapView, { PROVIDER_GOOGLE } from 'react-native-maps';
import { FlatList, ActivityIndicator } from 'react-native';
import * as Location from 'expo-location';
import HeaderIndex from '../components/headerIndex';
import TabBar from '../components/footer';
import CotacaoCard from '../components/cotacaoCard';


export default function CotacoesScreen() {
  const [region, setRegion] = useState(null);
  const mapRef = useRef(null);
  // const router = useRouter();
  const [cotacoes, setCotacoes] = useState([]);
  const [loading, setLoading] = useState(true);

  const params = useLocalSearchParams();

  console.log(params);

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
    (async () => {
      const date = new Date(params.horaSelecionada);
      const weekDay = date.getDay();
      const tipodia = weekDay === 0 || weekDay === 6 ? 'fim_de_semana' : 'dia_util';

      const ano = date.getFullYear();
      const mes = date.getMonth();
      const hora = date.getHours();
      const response = await fetch({
        url: "https://localhost:44394/api/RideQuotes", method: "POST", body: {
          OriginAddress: params.OriginAddress,
          DestinationAddress: params.destinationAddress,
          LatitudeOrigin: params.origemLat,
          LongitudeOrigin: params.origemLng,
          LatitudeDestination: params.destinationLat,
          LongitudeDestination: params.destinationLng,
          tipodia,
          tipohorario: 'livre', //mock
          ano,
          mes,
          hora,
          UserId: 1, // mock
        }
      });

      if (response.status === 200) {
        const data = await response.json();
        setCotacoes(data);
      }
    })();
  }, []);

  // if (loading) {
  //   return (
  //     <View style={styles.loading}>
  //       <ActivityIndicator size="large" color="#ED6FA9" />
  //     </View>
  //   );
  // }

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

        <Text style={styles.resultadosTitle}>Resultados</Text>

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
    top: 20,
    alignSelf: 'center',
    width: '90%',
    height: '35%',
    borderRadius: 12,
    borderWidth: 2,
    borderColor: '#D9D9D9',
    zIndex: 1,
  },
  flatListContent: {
    paddingTop: 200, // altura do mapa
    paddingBottom: 100, // altura do footer
    paddingHorizontal: 16,
  },
});
