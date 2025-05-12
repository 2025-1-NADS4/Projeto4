import React from "react";
import { View, StyleSheet, TouchableOpacity, TextInput} from "react-native";
import Icon from 'react-native-vector-icons/FontAwesome';


export default function inputCentral()
{
    return(
        <View style = {styles.container} >
            <TextInput style = {styles.InputLocal} placeholder = 'Origem'>
            
            </TextInput>
            <TouchableOpacity style = {styles.TrocarLabelSombra}>
                <View style = {styles.TrocarLabel}>
                    <Icon name="arrow-up"  color="black" />
                    <Icon name="arrow-down" color="black"/>    
                </View>
            </TouchableOpacity>
            <TextInput style = {styles.InputDestino} placeholder = 'Destino'>

            </TextInput>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        backgroundColor: '#D9D9D9',
        borderColor: '#A59F9F',
        borderWidth: 2,
        borderRadius: 20,
        width: 373,
        height: 163,
        flexDirection: 'column',
        alignItems: 'center',
        gap: 7,
        position: 'absolute',
        top: 125,
        zIndex: 9999
    }, 

    InputLocal: {
        flexDirection: 'row',
        borderTopLeftRadius: 20,
        borderTopRightRadius: 20,
        backgroundColor: 'white',
        marginTop: 8,
        width: 355,
        height: 70,      
    },

    InputDestino: {
        flexDirection: 'row',
        borderBottomLeftRadius: 20,
        borderBottomRightRadius:20,
        width: 355,
        height: 70,
        backgroundColor: 'white',
        position: 'absolute',
        bottom: 8,

    },

    TrocarLabelSombra: {
        height: 36,
        width: 36,
        backgroundColor: '#D9D9D9',
        borderRadius: 50,
        position: "absolute",
        top: 62,
        zIndex: 9999,
        alignItems: 'center',
        justifyContent: 'center',      
    },

    TrocarLabel:{
        flexDirection: 'row',
        height: 30,
        width: 30,
        backgroundColor: 'white',
        borderRadius: 80,
        alignItems: 'center',
        justifyContent: 'center'
    }



});