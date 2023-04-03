<template>
    <Chart :size="{ width: 500, height: 420 }"
           :data="data"
           :margin="margin"
           :direction="direction"
           :axis="axis">

        <template #layers>
            <Grid strokeDasharray="2,2" />
            <Line :dataKeys="['name', 'Bacardi']" :lineStyle="{ fill: '#e81010' }" />
            <Line :dataKeys="['name', 'Albert Heijn']" :lineStyle="{ fill: '#1a6aeb' }" />
            <Line :dataKeys="['name', 'Haribo']" :lineStyle="{ fill: '#700f56' }" />
            <Line :dataKeys="['name', 'GS1']" :lineStyle="{ fill: '#a0e810' }" />
            <Line :dataKeys="['name', 'DMS']" :lineStyle="{ fill: '#05f283' }" />
            <Line :dataKeys="['name', 'Kaas']" :lineStyle="{ fill: '#edddda' }" />
            <Line :dataKeys="['name', 'Speciaalbier']" :lineStyle="{ fill: '#e8a01a' }" />
            <Line :dataKeys="['name', 'Laptops']" :lineStyle="{ fill: '#3df50f' }" />
            <Line :dataKeys="['name', 'Etos']" :lineStyle="{ fill: '#044491' }" />
            <Line :dataKeys="['name', 'Logistics']" :lineStyle="{ fill: '#f545c6' }" />
            <Line :dataKeys="['name', 'Drogisterij baby']" :lineStyle="{ fill: '#3e4470' }" />
            <Line :dataKeys="['name', 'Tandpasta']" :lineStyle="{ fill: '#362a2d' }" />
        </template>

        <template #widgets>
        </template>

    </Chart>
</template>

<script lang="ts">
    import { defineComponent, ref } from 'vue'
    import {  Chart, Grid, Line } from 'vue3-charts'
import { FruitsData } from './Barchart.vue'

    export const FruitData = [
        { name: 'Mon', OrangeSales: 80, GrapeSales: 40, AppleSales: 10 },
        { name: 'Tue', OrangeSales: 90, GrapeSales: 40, AppleSales: 20 },
        { name: 'Wed', OrangeSales: 40, GrapeSales: 40, AppleSales: 30 },
        { name: 'Thu', OrangeSales: 10, GrapeSales: 40, AppleSales: 40 },
        { name: 'fri', OrangeSales: 10, GrapeSales: 40, AppleSales: 50 },
        { name: 'Sat', OrangeSales: 40, GrapeSales: 40, AppleSales: 60 },
        { name: 'Sun', OrangeSales: 90, GrapeSales: 40, AppleSales: 70 }
    ]
    export const MeatData = [
        { name: 'Mon', Pork: 80, Beef: 40},
        { name: 'Tue', Pork: 90, Beef: 40},
        { name: 'Wed', Pork: 40, Beef: 40},
        { name: 'Thu', Pork: 10, Beef: 40},
        { name: 'fri', Pork: 10, Beef: 40},
        { name: 'Sat', Pork: 40, Beef: 40},
        { name: 'Sun', Pork: 90, Beef: 40 }
    ]
 
    let chosen = ''

    let data = ref()
    export default defineComponent({
        name: 'LineChart',
        components: { Chart, Grid, Line },

        props: {
            myString: {
                required: true
            },
        },
        setup(props) {
            const direction = ref('horizontal')
            const margin = ref({
                left: 0,
                top: 20,
                right: 20,
                bottom: 0
            })

            const axis = ref({
                primary: {
                    type: 'band',
                    format: (val: string) => {
                        return val
                    }
                },
                secondary: {
                    domain: ['0', '100'],
                    type: 'linear',
                    ticks: 8
                }
            })
            let randomnumber: number
            function generateRandomNumber() {
               randomnumber = Math.floor(Math.random() * 81);
             }

            if (props.myString === 'Laptops') {
                const data = ref([])
                const fetchData = async () => {
                    const res = await fetch("https://localhost:5001/api/widget/category")
                    const finalRes = await res.json()
                    const laptopData =  finalRes.map((productname) => {
                        generateRandomNumber()
                        return { 
                            name: [value],
                            [productname]: randomnumber,
                            
                        }
                        
                    })
                    console.log(laptopData)
                    data.value = laptopData
                }
                fetchData()
                return { data, direction, margin, axis }
            } else if (props.myString === 'Meat') {
                const data = ref(MeatData)
                return { data, direction, margin, axis }
            } else {
                const data = ref([])
                return { data, direction, margin, axis }
            }
        }
    })
</script>