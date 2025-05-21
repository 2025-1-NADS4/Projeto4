import React, { useState } from 'react';
import { View, StyleSheet, TouchableOpacity, TextInput, Text, FlatList } from 'react-native';
import Icon from 'react-native-vector-icons/FontAwesome';
import { responsiveHeight } from 'react-native-responsive-dimensions';
import { Image } from 'react-native';
import DateTimePicker from '@react-native-community/datetimepicker';
import ImgViajarAgora from '../assets/viajar-agora.png';
import ImgViajarMaisTarde from '../assets/viajar-mais-tarde.png';
import { useRouter } from 'expo-router';


export default function InputCentral() {
  const [originInput, setOriginInput] = useState('');
  const [destinationInput, setDestinationInput] = useState('');
  const [originSuggestions, setOriginSuggestions] = useState([]);
  const [destinationSuggestions, setDestinationSuggestions] = useState([]);
  const GOOGLE_API_KEY = 'AIzaSyDtauS1lmtuMouZS5XFGlIlDEFZ64wWML0';

  const [mostrarHorario, setMostrarHorario] = useState(false);
  const [dataHora, setDataHora] = useState(new Date());

  const router = useRouter();
  const [originLocation, setOriginLocation] = useState();
  const [destinationLocation, setDestinationLocation] = useState();


  const fetchPlaces = async (inputText, setSuggestion) => {
    if (!inputText) {
      setSuggestion([]);
      return;
    }

    const url = `https://maps.googleapis.com/maps/api/place/autocomplete/json?input=${encodeURIComponent(
      inputText
    )}&key=${GOOGLE_API_KEY}&language=pt-BR&components=country:br`;

    try {
      const response = await fetch(url);
      const data = await response.json();

      if (data.status === 'OK') {
        setSuggestion(data.predictions);
      } else {
        console.warn('Erro na API:', data.status, data.error_message);
      }
    } catch (err) {
      console.error('Erro na requisição:', err);
    }
  };

  const trocarInput = () => {
    const origemTemp = originInput;
    setOriginInput(destinationInput);
    setDestinationInput(origemTemp);
    setOriginSuggestions([]);
    setDestinationSuggestions([]);
  };

  const handleSelectSuggestion = async (suggestion, setInput, setSuggestion, setLocation) => {
    const placeId = suggestion.place_id;
    const detailsUrl = `https://maps.googleapis.com/maps/api/place/details/json?place_id=${placeId}&key=${GOOGLE_API_KEY}`;

    try {
      const response = await fetch(detailsUrl);
      const data = await response.json();

      if (data.status === 'OK') {
        setLocation(data.result.geometry.location);
        setInput(suggestion.description);
        setSuggestion([]);
      } else {
        console.warn('Erro ao buscar detalhes do lugar:', data.status, data.error_message);
      }
    } catch (error) {
      console.error('Erro ao buscar coordenadas:', error);
    }
  };

  const handleViajar = () => {
    if (location) {
      router.push({
        pathname: '/cotacoes',
        params: {
          origemLat: originLocation?.lat,
          origemLng: originLocation?.lng,
          destinationLat: destinationLocation?.lat,
          destinationLng: destinationLocation?.lng,
          horaSelecionada: dataHora.toISOString(),
          destinationAddress: destinationInput,
          originAddress: originInput
        },
      });
    } else {
      alert('Localização não disponível');
    }
  };

  return (
    <React.Fragment>
      <View style={styles.container}>
        <TextInput
          style={styles.inputLocal}
          placeholder="MEU LOCAL DE PARTIDA"
          placeholderTextColor="#888"
          value={originInput}
          onChangeText={(text) => {
            setOriginInput(text);
            fetchPlaces(text, setOriginSuggestions);
          }}
        />
        {originInput.length && originSuggestions.length && (
          <FlatList
            data={originSuggestions}
            keyExtractor={(item) => item.place_id}
            style={[styles.suggestions, { top: responsiveHeight(18) }]}
            renderItem={({ item }) => (
              <TouchableOpacity onPress={() => handleSelectSuggestion(item, setOriginInput, setOriginSuggestions, setOriginLocation)} style={styles.suggestionItem}>
                <Text>{item?.description}</Text>
              </TouchableOpacity>
            )}
          />
        )}

        <TouchableOpacity
          style={styles.trocarLabelSombra}
          accessibilityLabel="Trocar origem e destino"
          accessibilityRole="button"
          onPress={trocarInput}
        >
          <View style={styles.trocarLabel}>
            <Icon name="arrow-up" color="black" />
            <Icon name="arrow-down" color="black" />
          </View>
        </TouchableOpacity>

        <TextInput
          style={styles.inputDestino}
          placeholder="MEU LOCAL DE DESTINO"
          placeholderTextColor="#888"
          value={destinationInput}
          onChangeText={(text) => {
            setDestinationInput(text);
            fetchPlaces(text, setDestinationSuggestions);
          }}
        />
        {destinationInput.length && destinationSuggestions.length && (
          <FlatList
            data={destinationSuggestions}
            keyExtractor={(item) => item.place_id}
            style={[styles.suggestions, { top: responsiveHeight(18) }]}
            renderItem={({ item }) => (
              <TouchableOpacity onPress={() => handleSelectSuggestion(item, setDestinationInput, setDestinationSuggestions, setDestinationLocation)} style={styles.suggestionItem}>
                <Text>{item?.description}</Text>
              </TouchableOpacity>
            )}
          />
        )}
      </View>

      {/* BUTTONS */}
      <View style={styles.buttonRow}>
        <TouchableOpacity
          style={styles.button}
          onPress={handleViajar}
        >
          <Image source={ImgViajarAgora} style={styles.image} />
          <Text style={styles.buttonText}>Viajar agora</Text>
        </TouchableOpacity>

        <TouchableOpacity
          style={styles.button}
          onPress={() => {
            setMostrarHorario(true);
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
              handleViajar();
            }}
          />
        )}
      </View>
    </React.Fragment>
  );
}

const styles = StyleSheet.create({
  container: {
    backgroundColor: '#D9D9D9',
    borderColor: '#A59F9F',
    borderWidth: 2,
    borderRadius: 20,
    width: '90%',
    height: responsiveHeight(18),
    flexDirection: 'column',
    alignItems: 'center',
    gap: 7,
    position: 'absolute',
    alignSelf: 'center',
    top: responsiveHeight(9),
    zIndex: 9999,
  },
  inputLocal: {
    borderTopLeftRadius: 20,
    borderTopRightRadius: 20,
    backgroundColor: 'white',
    marginTop: 8,
    width: '95%',
    height: '42%',
    paddingHorizontal: 12,
    fontSize: 14,
  },
  inputDestino: {
    borderBottomLeftRadius: 20,
    borderBottomRightRadius: 20,
    width: '95%',
    height: '42%',
    backgroundColor: 'white',
    position: 'absolute',
    bottom: 8,
    paddingHorizontal: 12,
    fontSize: 14,
  },
  trocarLabelSombra: {
    height: 36,
    width: 36,
    backgroundColor: '#D9D9D9',
    borderRadius: 50,
    position: 'absolute',
    top: '40%',
    zIndex: 9999,
    alignItems: 'center',
    justifyContent: 'center',
  },
  trocarLabel: {
    flexDirection: 'row',
    height: 30,
    width: 30,
    backgroundColor: 'white',
    borderRadius: 80,
    alignItems: 'center',
    justifyContent: 'center',
  },
  suggestions: {
    position: 'absolute',
    backgroundColor: 'white',
    width: '100%',
    alignSelf: 'center',
    zIndex: 10000,
    maxHeight: 600,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#aaa',
  },
  suggestionItem: {
    padding: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#ccc',
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
    fontSize: 11,
    position: "absolute",
    bottom: 15,

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
    position: "absolute",
    bottom: "29%",
  },
});
