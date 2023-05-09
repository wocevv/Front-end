<template>
    <Bar id="my-chart-id"
            :options="chartOptions"
            :data="chartData" />
</template>

<script>
    import { Bar } from 'vue-chartjs'
    import { Chart as ChartJS, Colors, Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale } from 'chart.js'
    import { defineComponent, ref, watchEffect, computed } from 'vue'


    ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)
    ChartJS.register(Colors)

    const graphdata = ref([])

    

    const labelArray = []
    const dataArray = []



    export default {
        name: 'BarChart',
        components: { Bar },
        data() {
            return {
                chartData: {
                    labels: labelArray,
                    datasets: [{
                        data: dataArray,
                        label: 'ProductCount'
                    }]
                },
                chartOptions: {
                    responsive: true,

                }
            }
        },
        props: {
            widgetdata: {
                type: Array,
            }
        },
        setup(props) {

            for (const item of props.widgetdata) {
                labelArray.push(item.group)
                dataArray.push(item.value)
            }
            console.log(props.widgetdata)
        }
    }
</script>