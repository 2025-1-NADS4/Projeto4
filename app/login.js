import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  SafeAreaView,
} from 'react-native';
import HeaderLogin from '../components/headerLogin';
import { Image } from 'react-native';
import { useRouter } from 'expo-router';

export default function LoginScreen() {
  const router = useRouter();
  const [emailCpf, setEmailCpf] = useState('');
  const [senha, setSenha] = useState('');

  const handleLogin = () => {
    // lógica de autenticação aqui
    console.log('Login com:', emailCpf, senha);
    // router.push('/alguma-rota'); // redirecionar se quiser
  };

  return (
    <SafeAreaView style={styles.container}>
      <HeaderLogin />

      <View style={styles.content}>
        <Text style={styles.titulo}>Seja bem vindo</Text>
        <Image
        source={require('../assets/logo.png')} // ajuste o caminho conforme seu projeto
         style={styles.logo}
        />
        <TextInput
          style={styles.input}
          placeholder="E-mail ou CPF"
          value={emailCpf}
          onChangeText={setEmailCpf}
          keyboardType="default"
          autoCapitalize="none"
        />

        <TextInput
          style={styles.input}
          placeholder="Senha"
          value={senha}
          onChangeText={setSenha}
          secureTextEntry
        />

        <TouchableOpacity style={styles.botao} onPress={() => router.push('/')}>
          <Text style={styles.botaoTexto}>Acessar</Text>
        </TouchableOpacity>

        <TouchableOpacity
          onPress={() => router.push('/cadastro')} // ou qualquer rota
          style={styles.linkContainer}
        >
          <Text style={styles.link}>
            Ainda não tem conta? <Text style={styles.linkNegrito}>Cadastre-se</Text>
          </Text>
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
  },
  content: {
    padding: 24,
    alignItems: 'center',
  },
  titulo: {
    fontSize: 30,
    fontWeight: 'bold',
    marginVertical: 24,
    textAlign: 'center',
    color: '#333',
  },
  input: {
    width: '100%',
    height: 50,
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 8,
    paddingHorizontal: 16,
    marginBottom: 16,
    fontSize: 16,
  },
  botao: {
    backgroundColor: '#DD0483',
    paddingVertical: 12,
    paddingHorizontal: 32,
    borderRadius: 20,
    marginTop: 8,
    width: '100%',
    alignItems: 'center',
  },
  botaoTexto: {
    color: '#fff',
    fontSize: 16,
    fontWeight: 'bold',
  },
  linkContainer: {
    marginTop: 32,
  },
  link: {
    fontSize: 14,
    color: '#444',
  },
  linkNegrito: {
    fontWeight: 'bold',
    color: '#007bff',
  },
  logo: {
  width: 350,
  height: 200,
  resizeMode: 'contain',
  marginBottom: 24,
},

});
