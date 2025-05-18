import { useRouter } from 'expo-router';
import React, { useState, useEffect, useRef} from 'react';
import { Image } from 'react-native';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView } from 'react-native';
import MapView, { PROVIDER_GOOGLE } from 'react-native-maps';
import * as Location from 'expo-location';
import HeaderIndex from '../components/headerIndex';
import InputCentral from '../components/inputCentral';
import TabBar from '../components/footer';
import DateTimePicker from '@react-native-community/datetimepicker';
import ImgViajarAgora from '../assets/viajar-agora.png';
import ImgViajarMaisTarde from '../assets/viajar-mais-tarde.png';

export default function HomeScreen() {
  const [location, setLocation] = useState(null);
  const [mostrarHorario, setMostrarHorario] = useState(false);
  const [dataHora, setDataHora] = useState(new Date());
  const [region, setRegion] = useState({
    latitude: -23.5505, 
    longitude: -46.6333,
    latitudeDelta: 0.0922,
    longitudeDelta: 0.0421,
  });
  const mapRef = useRef(null);
  const router = useRouter();

  useEffect(() => {
    const getLocation = async () => {
      const { status } = await Location.requestForegroundPermissionsAsync();
      if (status !== 'granted') {
        alert('Permissão para acessar localização negada');
        return;
      }

      Location.watchPositionAsync(
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
    };

    getLocation();

    return () => {
      Location.removeWatchAsync();
    };
  }, []);

  const handleViajarAgora = () => {
    if (location) {
      router.push({
        pathname: '/cotacoes',
        query: {
          origemLat: location.coords.latitude,
          origemLng: location.coords.longitude,
        },
      });
    } else {
      alert('Localização não disponível');
    }
  };

  const handleViajarMaisTarde = () => {
    if (location) {
      router.push({
        pathname: '/cotacoes',
        query: {
          origemLat: location.coords.latitude,
          origemLng: location.coords.longitude,
          horaSelecionada: dataHora.toISOString(),
        },
      });
    } else {
      alert('Localização não disponível');
    }
  };

  const mostrarRelogio = () => {
    setMostrarHorario(true);
  };

  return (
    <SafeAreaView style={styles.container}>
      <View style={{ width: '100%', position: 'relative' }}>
        <HeaderIndex />
        <InputCentral />
      </View>

      <View style={styles.buttonRow}>
        <TouchableOpacity
          style={styles.button}
          onPress={handleViajarAgora}
        >
          <Image source={ImgViajarAgora} style={styles.image} />
          <Text style={styles.buttonText}>Viajar agora</Text>
        </TouchableOpacity>

        <TouchableOpacity
          style={styles.button}
          onPress={() => {
            setMostrarHorario(true);
            mostrarRelogio();
          }}
        >
          <Image source={ImgViajarMaisTarde} style={styles.imageLater} />
          <Text style={styles.buttonText}>Viajar mais tarde</Text>
        </TouchableOpacity>

        {mostrarHorario && (
          <DateTimePicker
            value={dataHora}
            mode="time"
            is24Hour={true}
            display="default"
            onChange={(event, selectedDate) => {
              if (event.type === 'set' && selectedDate) {
                setDataHora(selectedDate);
              }
              setMostrarHorario(false);
              handleViajarMaisTarde();
            }}
          />
        )}
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
  buttonRow: {
    height: 200,
    flexDirection: 'row',
    justifyContent: 'center',
    gap: 50,
    width: '100%',
  },
  button: {
    height: 110,
    width: 110,
    justifyContent: 'center',
    alignItems: 'center',
    borderColor: '#D9D9D9',
    borderWidth: 2,
    top: 90,
    borderRadius: 20,
  },
  buttonText: {
    fontWeight: 'bold',
    textAlign: 'center',
    fontSize: "11",
    position: "absolute",
    bottom: 15,

  },
  map: {
    flex: 1,
    borderColor: '#D9D9D9',
    borderWidth: 1,
    marginTop: "3%",
    width: '100%',
  },
  image: {
  width: '60%', 
  height: '60%',
  resizeMode: 'contain', 
},
imageLater: {
  width: '60%',
  height: '60%',
  resizeMode: 'contain',
  marginLeft: 10, 
  position:"absolute", 
  bottom: "29%",
},

});
