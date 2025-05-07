import React, { useState, useEffect, useRef } from 'react';
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

const GOOGLE_API_KEY = 'AIzaSyDtauS1lmtuMouZS5XFGlIlDEFZ64wWML0'; // Substitua pela sua chave

export default function HomeScreen() {
  const [originCoordinates, setOriginCoordinates] = useState(null);
  const [destinationCoordinates, setDestinationCoordinates] = useState(null);
  const [location, setLocation] = useState(null);
  const [loading, setLoading] = useState(false);
  const mapRef = useRef(null);
  const [originFocused, setOriginFocused] = useState(false);
  const [destinationFocused, setDestinationFocused] = useState(false);
  
  const [originInput, setOriginInput] = useState('');
  const [destinationInput, setDestinationInput] = useState('');
  const [originSuggestions, setOriginSuggestions] = useState([]);
  const [destinationSuggestions, setDestinationSuggestions] = useState([]);

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

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.headerText}>FASOR</Text>
      </View>

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : undefined}>
        <View style={styles.inputsContainer}>
          {/* Campo Origem */}
          <TextInput
            style={styles.input}
            placeholder="Origem"
            value={originInput}
            onChangeText={(text) => {
              setOriginInput(text);
              fetchPlaces(text, true);
            }}
            onKeyPress={({ nativeEvent }) => {
              if (nativeEvent.key === 'Backspace' && originInput === 'Minha localização atual') {
                setOriginInput('');
                setOriginCoordinates(null);
                setOriginSuggestions([]);
              }
            }}
            onFocus={() => setOriginFocused(true)}
            onBlur={() => {
              setOriginFocused(false);
              setTimeout(() => setOriginSuggestions([]), 100); // timeout p/ evitar sumir ao clicar numa sugestão
            
            }}
          
          />
          <FlatList
            data={originSuggestions}
            keyExtractor={(item) => item.place_id}
            renderItem={({ item }) => (
              <TouchableOpacity onPress={() => getCoordinates(item.place_id, true)}>
                <Text style={styles.suggestionItem}>{item.description}</Text>
              </TouchableOpacity>
            )}
          />

          <TextInput
            style={styles.input}
            placeholder="Destino"
            value={destinationInput}
            onChangeText={(text) => {
              setDestinationInput(text);
              fetchPlaces(text, false); // false => campo destino
            }}
            onFocus={() => setDestinationFocused(true)}
            onBlur={() => {
              setDestinationFocused(false);
              setTimeout(() => setDestinationSuggestions([]), 100);
            }}
          />

          
          <FlatList
            data={destinationSuggestions}
            keyExtractor={(item) => item.place_id}
            renderItem={({ item }) => (
              <TouchableOpacity onPress={() => getCoordinates(item.place_id, false)}>
                <Text style={styles.suggestionItem}>{item.description}</Text>
              </TouchableOpacity>
            )}
          />

          <TouchableOpacity style={styles.button} onPress={setYourLocationAsOrigin} disabled={loading}>
            <Text style={styles.buttonText}>Usar minha localização</Text>
          </TouchableOpacity>
        </View>
      </KeyboardAvoidingView>

      <View style={styles.buttonRow}>
        <TouchableOpacity style={styles.button} disabled={loading}>
          <Text style={styles.buttonText}>Viajar agora</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.button} disabled={loading}>
          <Text style={styles.buttonText}>Viajar mais tarde</Text>
        </TouchableOpacity>
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
    backgroundColor: '#fff',
  },
  header: {
    backgroundColor: '#ff69b4',
    padding: 20,
  },
  headerText: {
    color: '#fff',
    fontSize: 24,
    textAlign: 'center',
  },
  inputsContainer: {
    padding: 16,
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
    flexDirection: 'row',
    justifyContent: 'space-around',
    marginVertical: 16,
  },
  button: {
    backgroundColor: '#ff69b4',
    paddingVertical: 10,
    paddingHorizontal: 20,
    borderRadius: 8,
  },
  buttonText: {
    color: '#fff',
    fontWeight: 'bold',
  },
  map: {
    flex: 1,
    margin: 16,
    borderColor: '#ccc',
    borderWidth: 1,
    borderRadius: 8,
  },
  footer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    paddingVertical: 16,
    borderTopWidth: 1,
    borderColor: '#ccc',
  },
  loadingContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
});
