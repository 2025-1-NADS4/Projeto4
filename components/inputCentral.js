import React, { useState, useRef } from 'react';
import { View, StyleSheet, TouchableOpacity, TextInput, Text, FlatList } from 'react-native';
import Icon from 'react-native-vector-icons/FontAwesome';
import { responsiveHeight } from 'react-native-responsive-dimensions';


export default function InputCentral() {
  const [originInput, setOriginInput] = useState('');
  const [destinationInput, setDestinationInput] = useState('');
  const [originSuggestions, setOriginSuggestions] = useState([]);
  const [destinationSuggestions, setDestinationSuggestions] = useState([]);
  const GOOGLE_API_KEY = 'AIzaSyDtauS1lmtuMouZS5XFGlIlDEFZ64wWML0';

  const originInputRef = useRef(null);
  const destinationInputRef = useRef(null);

  const fetchPlaces = async (inputText, isOrigin) => {
    if (!inputText) {
      isOrigin ? setOriginSuggestions([]) : setDestinationSuggestions([]);
      return;
    }

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

  const trocarInput = () => {
    const origemTemp = originInput;
    setOriginInput(destinationInput);
    setDestinationInput(origemTemp);
    setOriginSuggestions([]);
    setDestinationSuggestions([]);

    if (originInputRef.current) {
      originInputRef.current.blur();
    }

    if (destinationInputRef.current) {
      destinationInputRef.current.blur();
    }
  };

  const handleSelectSuggestion = async (suggestion, isOrigin) => {
    const placeId = suggestion.place_id;
    const detailsUrl = `https://maps.googleapis.com/maps/api/place/details/json?place_id=${placeId}&key=${GOOGLE_API_KEY}`;

    try {
      const response = await fetch(detailsUrl);
      const data = await response.json();

      if (data.status === 'OK') {
        const location = data.result.geometry.location;

        if (isOrigin) {
          setOriginInput(suggestion.description);
          setOriginSuggestions([]);
        } else {
          setDestinationInput(suggestion.description);
          setDestinationSuggestions([]);
        }
      } else {
        console.warn('Erro ao buscar detalhes do lugar:', data.status, data.error_message);
      }
    } catch (error) {
      console.error('Erro ao buscar coordenadas:', error);
    }
  };

  return (
    <View style={styles.container}>
      <TextInput
        ref={originInputRef}
        style={styles.inputLocal}
        placeholder="MEU LOCAL DE PARTIDA"
        placeholderTextColor="#888"
        value={originInput}
        onChangeText={(text) => {
          setOriginInput(text);
          fetchPlaces(text, true);
        }}
      />
      {originInput.length > 0 && originSuggestions.length > 0 && (
        <FlatList
          data={originSuggestions}
          keyExtractor={(item) => item.place_id}
          style={[styles.suggestions, { top: responsiveHeight(18) }]}
          renderItem={({ item }) => (
            <TouchableOpacity onPress={() => handleSelectSuggestion(item, true)} style={styles.suggestionItem}>
              <Text>{item.description}</Text>
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
        ref={destinationInputRef}
        style={styles.inputDestino}
        placeholder="MEU LOCAL DE DESTINO"
        placeholderTextColor="#888"
        value={destinationInput}
        onChangeText={(text) => {
          setDestinationInput(text);
          fetchPlaces(text, false);
        }}
      />
      {destinationInput.length > 0 && destinationSuggestions.length > 0 && (
        <FlatList
          data={destinationSuggestions}
          keyExtractor={(item) => item.place_id}
          style={[styles.suggestions, { top: responsiveHeight(18) }]}
          renderItem={({ item }) => (
            <TouchableOpacity onPress={() => handleSelectSuggestion(item, false)} style={styles.suggestionItem}>
              <Text>{item.description}</Text>
            </TouchableOpacity>
          )}
        />
      )}
    </View>
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
});
