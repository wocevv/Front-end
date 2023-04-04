<template>
    <Bar id="my-chart-id"
            :options="chartOptions"
            :data="chartData" />
</template>

<script>
    import { Bar } from 'vue-chartjs'
    import { Chart as ChartJS, Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale } from 'chart.js'

    ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

    const apiFetch = await fetch('https://localhost:5001/api/Widget/test2');
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
                    datasets: [{ data: dataArray }]
                },
                chartOptions: {
                    responsive: true
                }
            }
        }
    }
</script>