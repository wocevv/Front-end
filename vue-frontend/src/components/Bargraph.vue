<template>
    <Bar id="my-chart-id" :options="chartOptions" :data="chartData" />
</template>

<script>
    import { Bar } from 'vue-chartjs'
    import { Chart as ChartJS, Colors, Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale } from 'chart.js'
    import { defineComponent, ref, watchEffect, computed, watch } from 'vue'

    ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)
    ChartJS.register(Colors)

    const labelArray = []
    const dataArray = []

    export default {
        name: 'BarChart',
        components: { Bar },
        props: {
            widgetdata: {
                type: Array,
            },
            orientationValue: {
                type: Number,
            }
        },
        data() {
            return {
                chartData: {
                    labels: labelArray,
                    datasets: [
                        {
                            data: dataArray,
                            label: 'ProductCount'
                        }
                    ]
                },
                chartOptions: {
                    responsive: true,
                    indexAxis: 'y',
                    scales: {
                        x: {
                            beginAtZero: true
                        },
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            }
        },
        watch: {
            widgetdata: {
                handler(newVal) {
                    this.updateChartData(newVal)
                },
                immediate: true
            },
            orientationValue: {
                handler(newVal) {
                    this.updateChartOptions(newVal)
                },
                immediate: true
            }
        },
        methods: {
            updateChartOptions(orientationValue) {
                if (orientationValue === 1) {
                    this.chartOptions.indexAxis = 'x'
                } else if (orientationValue === 2) {
                    this.chartOptions.indexAxis = 'y'
                    // Swap the x and y scales
                    this.chartOptions.scales.x = { beginAtZero: true }
                    this.chartOptions.scales.y = { beginAtZero: true }
                }
            },
            updateChartData(data) {
                labelArray.length = 0
                dataArray.length = 0

                for (const item of data) {
                    labelArray.push(item.group)
                    dataArray.push(item.value)
                }

                this.chartData.labels = labelArray
                this.chartData.datasets[0].data = dataArray
            }
        }
    }
</script>