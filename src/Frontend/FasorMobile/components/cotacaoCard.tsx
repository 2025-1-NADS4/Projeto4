import React from 'react';
import { View, Text, Image, StyleSheet } from 'react-native';

interface CotacaoCardProps {
  logo: any; // Pode ser require(...) ou { uri: string }
  titulo: string;
  descricao: string;
  preco: string;
}

const CotacaoCard: React.FC<CotacaoCardProps> = ({ logo, titulo, descricao, preco }) => {
  return (
    <View style={styles.card}>
      <View style={styles.header}>
        <View style={styles.textContainer}>
        <Image source={logo} style={styles.logo} />
        <Text style={styles.titulo}>{titulo}</Text>
        </View>
        <View>
            <Text style={styles.preco}>{preco}</Text>
        </View>
      </View>
      
       
       
      <View style={styles.info}>
        <Text style={styles.descricao}>{descricao}</Text>
       
        
        
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  card: {
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 5,
    padding: 12,
    margin: 6,
    backgroundColor: '#fff',
    shadowColor: '#000',
    shadowOpacity: 0.05,
    shadowOffset: { width: 0, height: 2 },
    shadowRadius: 4,
    elevation: 2,
  },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 8,
    justifyContent: 'space-between',

  },
  logo: {
    width: 50,
    height: 50,
    borderRadius: 20,
    marginRight: 10,
  },
  titulo: {
    fontSize: 16,
    fontWeight: 'bold',
  },
  info: {
    flexDirection: 'row',
    //justifyContent: 'space-between',
    //backgroundColor: 'red',
    alignItems: 'center', 
  },
  descricao: {
    fontStyle: 'italic',
    color: '#555',
  },

  textContainer:  {
    flexDirection: 'row',
    alignItems: 'center',

  },
 
  preco: {
    fontWeight: 'bold',
    color: '#00000',
    //position: 'absolute',
    //top: '50%',
    //left: '90%'
  },
});

export default CotacaoCard;
