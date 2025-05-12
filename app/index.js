import { useRouter } from 'expo-router';
import React, { useState, useEffect, useRef } from 'react';
import { TouchableWithoutFeedback, Keyboard } from 'react-native';
import DateTimePicker from '@react-native-community/datetimepicker';
import {
  View,
  Text,
  TouchableOpacity,
  StyleSheet,
  SafeAreaView,
  ActivityIndicator,
  KeyboardAvoidingView,
  Platform,
  TextInput,
  FlatList,
} from 'react-native';
import MapView, { Marker, PROVIDER_GOOGLE } from 'react-native-maps';
import * as Location from 'expo-location';
import HeaderIndex from '../components/headerIndex';
import InputCentral from '../components/inputCentral';

const GOOGLE_API_KEY = 'AIzaSyDtauS1lmtuMouZS5XFGlIlDEFZ64wWML0'; // Substitua pela sua chave

export default function HomeScreen() {
  const [originCoordinates, setOriginCoordinates] = useState(null);
  const [destinationCoordinates, setDestinationCoordinates] = useState(null);
  const [location, setLocation] = useState(null);
  const [loading, setLoading] = useState(false);
  const mapRef = useRef(null);
  const router = useRouter();

  const [originFocused, setOriginFocused] = useState(false);
  const [destinationFocused, setDestinationFocused] = useState(false);
  const [horaSelecionada, setHoraSelecionada] = useState(null);
  

  
  const [originInput, setOriginInput] = useState('');
  const [destinationInput, setDestinationInput] = useState('');
  const [originSuggestions, setOriginSuggestions] = useState([]);
  const [destinationSuggestions, setDestinationSuggestions] = useState([]);
  const [mostrarHorario, setMostrarHorario] = useState(false);
  const [dataHora, setDataHora] = useState(new Date());
  const handleViajar = (dataHora) => {
    // Coordenadas fictícias. Troque por Location.geocodeAsync ou Places API se quiser.
    const origemCoords = { latitude: -23.5, longitude: -46.6 };
    const destinoCoords = { latitude: -23.6, longitude: -46.7 };
  }

  const mostrarRelogio = () => {
    setMostrarHorario(true); // mostra o componente condicionalmente
      };
    

  useEffect(() => {
    (async () => {
      setLoading(true);
      let { status } = await Location.requestForegroundPermissionsAsync();
      if (status !== 'granted') {
        alert('Permissão de localização negada');
        setLoading(false);
        return;
      }
      let location = await Location.getCurrentPositionAsync({});
      setLocation(location);
      setLoading(false);
    })();
  }, []);

  const fetchPlaces = async (inputText, isOrigin) => {
    if (!inputText) return;

    const url = `https://maps.googleapis.com/maps/api/place/autocomplete/json?input=${encodeURIComponent(
      inputText
    )}&key=${GOOGLE_API_KEY}&language=pt-BR&components=country:br`;

    try {
      const response = await fetch(url);
      const data = await response.json();

      if (data.status === 'OK') {
        isOrigin ? setOriginSuggestions(data.predictions) : setDestinationSuggestions(data.predictions);
      } else {
        console.warn('Erro na API:', data.status, data.error_message);
      }
    } catch (err) {
      console.error('Erro na requisição:', err);
    }
  };

  const getCoordinates = async (placeId, isOrigin) => {
    const url = `https://maps.googleapis.com/maps/api/place/details/json?place_id=${placeId}&key=${GOOGLE_API_KEY}&language=pt-BR`;

    try {
      const response = await fetch(url);
      const data = await response.json();

      if (data.status === 'OK') {
        const loc = data.result.geometry.location;
        const coords = {
          latitude: loc.lat,
          longitude: loc.lng,
        };

        if (isOrigin) {
          setOriginCoordinates(coords);
          setOriginInput(data.result.name);
          setOriginSuggestions([]);
          mapRef.current?.animateToRegion({
            latitude: coords.latitude,
            longitude: coords.longitude,
            latitudeDelta: 0.01,
            longitudeDelta: 0.01,
          }, 1000);
        } else {
          setDestinationCoordinates(coords);
          setDestinationInput(data.result.name);
          setDestinationSuggestions([]);
          mapRef.current?.animateToRegion({
            latitude: coords.latitude,
            longitude: coords.longitude,
            latitudeDelta: 0.01,
            longitudeDelta: 0.01,
          }, 1000);
        }
      }
    } catch (err) {
      console.error('Erro ao obter coordenadas:', err);
    }
  };

  const setYourLocationAsOrigin = () => {
    if (location && location.coords) {
      const coords = {
        latitude: location.coords.latitude,
        longitude: location.coords.longitude,
      };
  
      setOriginCoordinates(coords);
      setOriginInput('Minha localização atual');
      setOriginSuggestions([]);
  
      mapRef.current?.animateToRegion({
        ...coords,
        latitudeDelta: 0.01,
        longitudeDelta: 0.01,
      }, 1000);
    } else {
      console.warn("Localização ainda não carregada.");
    }
  };

  const handleViajarAgora = () => {
    // Verifica se as coordenadas de origem e destino estão definidas
    if (originCoordinates && destinationCoordinates) {
      router.push({
        pathname: '/cotacoes', // Supondo que sua tela de cotações seja chamada '/cotações'
        query: {
          origemLat: originCoordinates.latitude,
          origemLng: originCoordinates.longitude,
          destinoLat: destinationCoordinates.latitude,
          destinoLng: destinationCoordinates.longitude,
        }
      });
    } else {
      alert('Defina a origem e o destino antes de viajar.');
    }
  };

  const handleViajarMaisTarde = () => {
    // Mesmo fluxo de viajar agora, mas com a hora selecionada
    if (originCoordinates && destinationCoordinates) {
      router.push({
        pathname: '/cotacoes', 
        query: {
          origemLat: originCoordinates.latitude,
          origemLng: originCoordinates.longitude,
          destinoLat: destinationCoordinates.latitude,
          destinoLng: destinationCoordinates.longitude,
          horaSelecionada: dataHora.toISOString(), // Passando a hora selecionada para a próxima tela
        }
      });
    } else {
      alert('Defina a origem e o destino antes de viajar.');
    }
  };


  return (
    <SafeAreaView style={styles.container}>

      <HeaderIndex/>
      <InputCentral></InputCentral>
      <View style={styles.buttonRow}>
        <TouchableOpacity style={styles.button} onPress={handleViajarAgora} disabled={loading}>
          <Text style={styles.buttonText}>Viajar agora</Text>
        </TouchableOpacity>
        <TouchableOpacity
          style={styles.button}
          onPress={() => {
            setMostrarHorario(true);
            mostrarRelogio();
          }}
          disabled={loading}
        >
          <Text style={styles.buttonText}>Viaje mais tarde</Text>
        </TouchableOpacity>
        {mostrarHorario && (
        <DateTimePicker
          value={dataHora}
          mode="time"
          is24Hour={true}
          display="default"
          onChange={(event, selectedDate) => {
            if (event.type === "set" && selectedDate) {
              setDataHora(selectedDate);
              const hora = selectedDate.getHours();
              const minuto = selectedDate.getMinutes();
              setHoraSelecionada(`${hora}:${minuto < 10 ? `0${minuto}` : minuto}`);
            }
            setMostrarHorario(false); // esconde após selecionar ou cancelar
            handleViajarMaisTarde();
          }}
        />
      )}
      
      </View>
      

      {loading ? (
        <View style={styles.loadingContainer}>
          <ActivityIndicator size="large" color="#ff69b4" />
        </View>
      ) : (
        <MapView
          ref={mapRef}
          provider={PROVIDER_GOOGLE}
          style={styles.map}
          initialRegion={{
            latitude: location?.coords?.latitude || -23.5505,
            longitude: location?.coords?.longitude || -46.6333,
            latitudeDelta: 0.0922,
            longitudeDelta: 0.0421,
            
          }}
        >
          {originCoordinates && (
            <Marker coordinate={originCoordinates} title="Origem" pinColor="#ff69b4" />
          )}
          {destinationCoordinates && (
            <Marker coordinate={destinationCoordinates} title="Destino" />
          )}
          
        </MapView>
      )}

      <View style={styles.footer}>
        <Text>🏠</Text>
        <Text>👤</Text>
        <Text>⚙️</Text>
      </View>
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

  inputsContainer: {
    justifyContent: 'center',
    alignItems: 'center'
    // padding: 16,
  },
  input: {
    height: 44,
    color: '#5d5d5d',
    fontSize: 16,
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 8,
    paddingHorizontal: 10,
    marginBottom: 8,
  },
  suggestionItem: {
    padding: 10,
    backgroundColor: '#f9f9f9',
    borderBottomColor: '#ccc',
    borderBottomWidth: 1,
  },
  buttonRow: {
    height: 200,
    flexDirection: 'row',
    justifyContent: 'center',
    gap: 50,
    marginVertical: 16,
    width: '100%',
  },
  button: {
    height: 110,
    width: 110,
    justifyContent: 'center',
    textAlign: 'center',
    alignItems: 'center',
    borderColor: '#D9D9D9',
    borderWidth: 2,
    top: 90,
    borderRadius: 20,
  },
  buttonText: {
    fontWeight: 'bold',
  },
  map: {
    flex: 1,
    borderColor: '#D9D9D9',
    borderWidth: 1,
    width: '100%',
  },
  footer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    paddingVertical: 16,
    borderTopWidth: 1,
    borderColor: '#ccc',
    width: '100%',
  },
  loadingContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
});
