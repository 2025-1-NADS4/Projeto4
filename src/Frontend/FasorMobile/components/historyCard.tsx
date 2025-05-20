import React from 'react';
import { View, Text, Image, StyleSheet, TouchableOpacity } from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const HistoryCard = ({ logo, titulo, dataHora, origem, destino, onDelete }) => {
  return (
    <View style={styles.card}>
      <View style={styles.content}>
        {/* Lateral esquerda com logo */}
        <Image source={logo} style={styles.logo} />

        {/* Coluna direita com informações */}
        <View style={styles.rightContent}>
          <View style={styles.header}>
            <Text style={styles.titulo}>{titulo}</Text>
            <Text style={styles.data}>{dataHora}</Text>
          </View>

          <Text style={styles.endereco}>Origem: {origem}</Text>

          {/* Linha com destino e lixeira */}
          <View style={styles.destinoRow}>
            <Text style={styles.endereco}>Destino: {destino}</Text>
            <TouchableOpacity onPress={onDelete} style={styles.trashButton}>
              <Ionicons name="trash-outline" size={22} color="#ED6FA9" />
            </TouchableOpacity>
          </View>

        </View>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  card: {
    borderWidth: 1,
    borderColor: '#ddd',
    borderRadius: 8,
    padding: 8,
    margin: 8,
    backgroundColor: '#fff',
    elevation: 2,
  },
  content: {
    flexDirection: 'row',
  },
  logo: {
    width: 50,
    height: 50,
    borderRadius: 20,
    marginRight: 10,
    justifyContent: 'center',
  },
  rightContent: {
    flex: 1,
    flexDirection: 'column',
    justifyContent: 'space-between',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  titulo: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#333',
    flex: 1,
  },
  data: {
    fontSize: 13,
    color: '#888',
    marginLeft: 8,
  },
  endereco: {
    fontSize: 14,
    color: '#555',
    marginTop: 2,
  },
  destinoRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginTop: 2,
  },
  trashButton: {
    paddingLeft: 10,
  },
});

export default HistoryCard;
