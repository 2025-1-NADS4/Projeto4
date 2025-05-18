import { useRouter } from 'expo-router';
import React from 'react';
import { View, Text, FlatList } from 'react-native';
import { useLocalSearchParams } from 'expo-router';
import MapView, { Marker } from 'react-native-maps';
import MapViewDirections from 'react-native-maps-directions';

const GOOGLE_MAPS_APIKEY = 'AIzaSyDtauS1lmtuMouZS5XFGlIlDEFZ64wWML0';

export default function CotacoesScreen() {
  const { origemLat, origemLng, destinoLat, destinoLng, horario } = useLocalSearchParams();
  const router = useRouter();

  const origin = {
    latitude: parseFloat(origemLat),
    longitude: parseFloat(origemLng),
  };

  const destination = {
    latitude: parseFloat(destinoLat),
    longitude: parseFloat(destinoLng),
  };

  const cotacoes = [
    { id: '1', nome: 'Motorista A', preco: 'R$ 25,00' },
    { id: '2', nome: 'App X', preco: 'R$ 20,50' },
    { id: '3', nome: 'Transporte Y', preco: 'R$ 22,90' },
  ];

  return (
    <View style={{ flex: 1 }}>
      <MapView
        style={{ flex: 2 }}
        initialRegion={{
          latitude: origin.latitude,
          longitude: origin.longitude,
          latitudeDelta: 0.05,
          longitudeDelta: 0.05,
        }}
      >
        <Marker coordinate={origin} title="Origem" />
        <Marker coordinate={destination} title="Destino" />
        <MapViewDirections
          origin={origin}
          destination={destination}
          apikey={GOOGLE_MAPS_APIKEY}
          strokeWidth={4}
          strokeColor="blue"
        />
      </MapView>

      <View style={{ flex: 1, padding: 10 }}>
        <Text style={{ fontWeight: 'bold', marginBottom: 10 }}>Cotações disponíveis:</Text>
        <FlatList
          data={cotacoes}
          keyExtractor={(item) => item.id}
          renderItem={({ item }) => (
            <Text>{`${item.nome} - ${item.preco}`}</Text>
          )}
        />
      </View>
    </View>
  );
}
