<template>
    <Chart :size="{ width: 2500, height: 500 }"
           :data="data"
           :margin="margin"
           :direction="direction"
           :axis="axis">

        <template #layers>
            <Grid strokeDasharray="2,2" />
            <Bar :dataKeys="['name', 'Bacardi']" :barStyle="{ fill: '#e81010'}" />
            <Bar :dataKeys="['name', 'Albert Heijn']" :barStyle="{ fill: '#1a6aeb' }" />
            <Bar :dataKeys="['name', 'Haribo']" :barStyle="{ fill: '#700f56' }" />
            <Bar :dataKeys="['name', 'GS1']" :barStyle="{ fill: '#a0e810' }" />
            <Bar :dataKeys="['name', 'DMS']" :barStyle="{ fill: '#05f283' }" />
            <Bar :dataKeys="['name', 'Kaas']" :barStyle="{ fill: '#edddda' }" />
            <Bar :dataKeys="['name', 'Speciaalbier']" :barStyle="{ fill: '#e8a01a' }" />
            <Bar :dataKeys="['name', 'Laptops']" :barStyle="{ fill: '#3df50f' }" />
            <Bar :dataKeys="['name', 'Etos']" :barStyle="{ fill: '#044491' }" />
            <Bar :dataKeys="['name', 'Logistics']" :barStyle="{ fill: '#f545c6' }" />
            <Bar :dataKeys="['name', 'Drogisterij baby']" :barStyle="{ fill: '#3e4470' }" />
            <Bar :dataKeys="['name', 'Tandpasta']" :barStyle="{ fill: '#362a2d' }" />
            <Bar :dataKeys="['name', 'Master Catalog']" :barStyle="{ fill: '#FFA500' }" />
            <Bar :dataKeys="['name', 'Drogisterij, baby']" :barStyle="{ fill: '#FFA500' }" />
            <Bar :dataKeys="['name', 'Tandpasta']" :barStyle="{ fill: '#FFA500' }" />
            <Bar :dataKeys="['name', 'Eten, drinken']" :barStyle="{ fill: '#FFA500' }" />
            <Bar :dataKeys="['name', 'Vis']" :barStyle="{ fill: '#FFA500' }" />
            <Bar :dataKeys="['name', 'Pasta, rijst, noedels']" :barStyle="{ fill: '#FFA500' }" />
            <Bar :dataKeys="['name', 'Olie en frituurvet']" :barStyle="{ fill: '#FFA500' }" />
            <Bar :dataKeys="['name', 'Snoep, koek, chips']" :barStyle="{ fill: '#FFA500' }" />
            <Bar :dataKeys="['name', 'Siroop']" :barStyle="{ fill: '#FFA500' }" />
            <Bar :dataKeys="['name', 'Vernice Gusikowsk']" :barStyle="{ fill: '#FFA500' }" />
            <Bar :dataKeys="['name', 'Shana Raynor Sr.']" :barStyle="{ fill: '#FFA500' }" />
        </template>

        <template #widgets>
            <Tooltip borderColor="#48CAE4"
                     :config="{ 
                        pl: { color: '#90e0ef' },
                        avg: { color: '#0096c7' },
                        inc: { color: '#48cae4' }}" />
        </template>

    </Chart>
</template>

<script lang="ts">
    import { defineComponent, ref } from 'vue'
    import { Bar, Chart, Grid, Tooltip, Marker } from 'vue3-charts'
    import { FruitData } from './Linegraph.vue'

    export const FruitsData = [
        { name: 'Mon', OrangeSales: 80 },
        { name: 'Mon', GrapeSales: 40, AppleSales: 10 },
        { name: 'Tue', OrangeSales: 90, GrapeSales: 40, AppleSales: 20 },
        { name: 'Wed', OrangeSales: 40, GrapeSales: 40, AppleSales: 30 },
        { name: 'Thu', OrangeSales: 10, GrapeSales: 40, AppleSales: 40 },
        { name: 'fri', OrangeSales: 10, GrapeSales: 40, AppleSales: 50 },
        { name: 'Sat', OrangeSales: 40, GrapeSales: 40, AppleSales: 60 },
        { name: 'Sun', OrangeSales: 90, GrapeSales: 40, AppleSales: 70 }
    ]
    export const MeatData = [
        { name: 'Mon', Pork: 80, Beef: 40 },
        { name: 'Tue', Pork: 90, Beef: 40 },
        { name: 'Wed', Pork: 40, Beef: 40 },
        { name: 'Thu', Pork: 10, Beef: 40 },
        { name: 'fri', Pork: 10, Beef: 40 },
        { name: 'Sat', Pork: 40, Beef: 40 },
        { name: 'Sun', Pork: 90, Beef: 40 }
    ]

  
    export default defineComponent({
        name: 'LineChart',
        components: { Chart, Grid, Bar , Tooltip, Marker},

        props: {
            myString: {
                type: String,
                required: true
            }
        },
        data() {
            return {
            }
        },
        methods: {
            // async getData() {
            //    const res = await fetch("https://localhost:5001/api/widget/category");
            //    const finalRes = await res.json();
            //    this.ProductList = finalRes;
            //    const LaptopData: { name: string; value: number } []=
            //        this.ProductList.map((value, index) => {
            //            return {
            //                name: "mon",
            //                value: value
            //            };
            //        });  
            //    return LaptopData
            //}
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
                    const res = await fetch("https://localhost:5001/api/widget/ProductCount")
                    const finalRes = await res.json()
                    const laptopData = finalRes.map((value) => {
                        generateRandomNumber()
                        return {
                            name: [value],
                            [value]: randomnumber,
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