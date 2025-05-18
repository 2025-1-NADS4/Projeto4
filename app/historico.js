import { useRouter } from 'expo-router';
import React, { useState } from 'react';
import {
  View,
  StyleSheet,
  SafeAreaView,
  FlatList,
  Alert,
  Text,
} from 'react-native';
import HeaderIndex from '../components/headerIndex';
import HistoryCard from '../components/historyCard';
import TabBar from '../components/footer';

export default function HistoricoScreen() {
  const router = useRouter();
  const nomeUsuario = 'Braga';
  const [historico, setHistorico] = useState([

    {
      id: '1',
      logo: require('../assets/logo_99.png'),
      titulo: 'Corrida 99Pop',
      dataHora: '17/05/2025 - 10:00',
      origem: 'Av. Faria Lima, 400 - SP',
      destino: 'Rua Augusta, 1500 - SP',
    },

    {
      id: '2',
      logo: require('../assets/logo_uber.png'),
      titulo: 'Corrida UberX',
      dataHora: '18/05/2025 - 14:30',
      origem: 'Av. Paulista, 1000 - São Paulo, SP',
      destino: 'Rua Vergueiro, 2000 - São Paulo, SP',
    },
  ]);

  const handleDelete = (id) => {
    Alert.alert(
      'Excluir corrida',
      'Tem certeza que deseja excluir este histórico?',
      [
        { text: 'Cancelar', style: 'cancel' },
        {
          text: 'Excluir',
          style: 'destructive',
          onPress: () => {
            setHistorico((prev) => prev.filter((item) => item.id !== id));
          },
        },
      ]
    );
  };

  return (
    <View style={{ flex: 1 }}>
      <SafeAreaView style={styles.container}>
        <View style={{ width: '100%', position: 'relative' }}>
            
        <HeaderIndex />
        <View style={styles.HeaderContainer}>
        <Text style={styles.titulo}>Olá, {nomeUsuario}!</Text>
        </View>
        </View>

         

        <FlatList
          data={historico}
          keyExtractor={(item) => item.id}
          contentContainerStyle={{ paddingBottom: 80 }}
          renderItem={({ item }) => (
            <HistoryCard
              logo={item.logo}
              titulo={item.titulo}
              dataHora={item.dataHora}
              origem={item.origem}
              destino={item.destino}
              onDelete={() => handleDelete(item.id)}
            />
          )}
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
  titulo: {
  fontSize: 35,
  fontWeight: 'bold',
  textAlign: 'center',
  marginVertical: 16,
  color: '#FFFFFF',
},

HeaderContainer: {
    position: 'absolute',
    top: 16, // ajuste fino conforme a altura do seu header
    width: '100%',
    transform: [{ translateY: 30 }],
    alignItems: 'center', 
}

});
