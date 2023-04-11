<template>
    <Bar id="my-chart-id"
            :options="chartOptions"
            :data="chartData" />
</template>

<script>
    import { Bar } from 'vue-chartjs'
    import { Chart as ChartJS, Colors, Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale } from 'chart.js'

    ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)
    ChartJS.register(Colors)


    const apiFetch = await fetch('https://localhost:5001/api/Widget/ProductCountTest');
    const jsonApiFetch = await apiFetch.json();

    const labelArray = []
    const dataArray = []

    for (const item of jsonApiFetch) {
        labelArray.push(item.month)
        dataArray.push(item.value)
    }

    export default {
        name: 'BarChart',
        components: { Bar },
        data() {
            return {
                chartData: {
                    labels: labelArray,
                    datasets: [{
                        data: dataArray,
                        label: 'Yo Homies'
                    }]
                },
                chartOptions: {
                    responsive: true,

                }
            }
        }
    }
</script>