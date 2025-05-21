import { useRouter } from 'expo-router';
import React, { useState, useEffect, useRef } from 'react';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView } from 'react-native';
import MapView, { PROVIDER_GOOGLE } from 'react-native-maps';
import * as Location from 'expo-location';
import HeaderIndex from '../components/headerIndex';
import InputCentral from '../components/inputCentral';
import TabBar from '../components/footer';


export default function HomeScreen() {
  const [location, setLocation] = useState(null);

  const [region, setRegion] = useState({
    latitude: -23.5505,
    longitude: -46.6333,
    latitudeDelta: 0.0922,
    longitudeDelta: 0.0421,
  });
  const mapRef = useRef(null);

  useEffect(() => {
    (async () => {
      const { status } = await Location.requestForegroundPermissionsAsync();
      if (status !== 'granted') {
        alert('Permissão para acessar localização negada');
        return;
      }

      subscription = await Location.watchPositionAsync(
        { accuracy: Location.Accuracy.Balanced, timeInterval: 1000, distanceInterval: 10 },
        (locationData) => {
          setLocation(locationData);
          setRegion({
            ...region,
            latitude: locationData.coords.latitude,
            longitude: locationData.coords.longitude,
          });
        }
      );
    })();

    return () => {
      if (subscription) subscription.remove();
    };
  }, []);



  return (
    <SafeAreaView style={styles.container}>
      <View style={{ width: '100%', position: 'relative' }}>
        <HeaderIndex />
        <InputCentral />
      </View>

      <MapView
        ref={mapRef}
        provider={PROVIDER_GOOGLE}
        style={styles.map}
        region={region}
        zoomEnabled={true}
        showsUserLocation={true}
        scrollEnabled={true}
        rotateEnabled={true}
        pitchEnabled={true}
        showsCompass={true}
      >
      </MapView>

      <TabBar />
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    flexDirection: 'column',
    backgroundColor: '#fff',
  },
  map: {
    flex: 1,
    borderColor: '#D9D9D9',
    borderWidth: 1,
    marginTop: "3%",
    width: '100%',
  },
});
