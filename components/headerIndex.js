import react from 'react';
import { View, Image } from 'react-native';
import Header from '../assets/HeaderIndex.png';

export default function HeaderIndex() {
  return (
    <View>
      <Image source={Header}/>
    </View>
  );
}