import React, { useState } from 'react';
import { View, TouchableOpacity, Animated, Dimensions, StyleSheet } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { useRouter } from 'expo-router';

const { width: screenWidth } = Dimensions.get('window');
const tabIcons: Array<'home-outline' | 'person-outline' | 'settings-outline'> = ['home-outline', 'person-outline', 'settings-outline'];
const tabIconsActive: Array<'home' | 'person' | 'settings'> = ['home', 'person', 'settings'];
const tabCount = tabIcons.length;
const tabWidth = screenWidth / tabCount;
const circleSize = 70;



const Footer = () => {
  const [activeIndex, setActiveIndex] = useState(0);
  const translateX = React.useRef(new Animated.Value(0)).current;
  const router = useRouter();

const moveIndicator = (index: number) => {
  Animated.spring(translateX, {
    toValue: tabWidth * index + tabWidth / 2 - circleSize / 2,
    useNativeDriver: true,
    damping: 10,
    stiffness: 100,
  }).start();
};



  React.useEffect(() => {
    moveIndicator(activeIndex);
  }, [activeIndex]);

  return (
    <View style={styles.container}>
      <View style={styles.footer}>
        <Animated.View
          style={[
            styles.activeCircle,
            {
              transform: [{ translateX }],
            },
          ]}
        >
          <Ionicons name={tabIconsActive[activeIndex]} size={30} color="#fff" />
        </Animated.View>

        <View style={styles.tabs}>
          {tabIcons.map((icon, index) => (
            <TouchableOpacity
              key={index}
              style={styles.tabButton}
              onPress={() => {
                setActiveIndex(index);
                moveIndicator(index);
                if (index === 0) {
                  router.push('/'); // Redireciona para index.js
                }else if (index == 1){
                  router.push('/historico');
                }
                else if (index == 2){
                  router.push('/login');
                }
              }}
            >
              <Ionicons
                name={icon}
                size={30}
                color={index === activeIndex ? '#fff' : '#444'}
              />
            </TouchableOpacity>
          ))}
        </View>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    position: 'absolute',
    bottom: 0,
    width: screenWidth,
    alignItems: 'center',
    justifyContent: 'center',
  },
  footer: {
    flexDirection: 'row',
    backgroundColor: '#fff',
    height: 70,
    borderRadius: 30,
    shadowOffset: { width: 0, height: -3 },
    shadowRadius: 6,
  },
  tabs: {
    flexDirection: 'row',
    justifyContent: 'space-between', 
  },
  tabButton: {
    width: tabWidth,
    alignItems: 'center',
    justifyContent: 'center',
    zIndex: 1,
  },
  activeCircle: {
  position: 'absolute',
  top: -15,
  width: circleSize,
  height: circleSize,
  backgroundColor: '#ED6FA9',
  borderRadius: circleSize / 2,
  alignItems: 'center',
  justifyContent: 'center',
  zIndex: 10,
  borderWidth: 0.6,
  borderColor: 'white',

  // iOS
  shadowColor: '#000',
  shadowOpacity: 0.4,
  shadowOffset: { width: 0, height: 4 },
  shadowRadius: 8,

  // Android
  elevation: 15,
},

}

);

export default Footer;