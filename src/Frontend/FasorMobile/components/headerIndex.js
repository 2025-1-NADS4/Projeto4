import React from 'react';
import { View, Image, StyleSheet } from 'react-native';
import { responsiveHeight } from 'react-native-responsive-dimensions';
import Header from '../assets/HeaderIndex.png';

export default function HeaderIndex() {
  return (
    <View style={styles.container}>
      <Image source={Header} style={styles.image} resizeMode="cover" />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    height: responsiveHeight(18),
    width: '100%',
  },
  image: {
    width: '100%',
    height: '100%',
  },
});