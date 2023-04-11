<script>

    let myString = "";
    import linegraph from '../../src/components/Linegraph.vue'
    import bargraph from '../../src/components/Barchart.vue'

    import router from '@/router'
    import { defineComponent, ref, watchEffect, computed } from 'vue'
    let Datamodel = ""
    let ShowWhatData = ""
    let GroupBy = ""
    let Datarange = ""
    let ChosenGraph = ""
    let StartDate = ""
    let EndDate = ""
    const widgets = ref([])

    export default {
        name: 'Datepicker',
            components: {
            linegraph,
            bargraph
            },
        data() {
            return {
                test: true,
                lineChartActive: false,
                barChartActive: false,
                listItems: []
            }
        },
        methods: {
            async getData() {
                const res = await fetch("https://localhost:5001/api/widget/category");
                const finalRes = await res.json();
                this.listItems = finalRes;
            },
            Gonext(id) {
                myString = id   
                router.push({ name: 'test', params: { myString } })             
            }
        },
        mounted() {
            this.getData()
        },
        
        props: {
            selectedOption: {
                type: String,
                required: true
            },
            selectedChart: {
                type: String,
                required: true
            }
        },
        setup(props) {
            if (props.selectedOption === null ) {
                selectedOption = 'undefined'
            }
            if (props.selectedChart === null ) {
                selectedChart = 'undefined'
            }
        },
        setup() {
            const startDate = ref('');
            const endDate = ref('');

            const minDate = computed(() => {
                return '2023-04-03'; // set the minimum date to April 3, 2023
            });

            const maxDate = computed(() => {
                return '2023-04-10'; // set the maximum date to April 10, 2023
            });

            return {
                startDate,
                endDate,
                minDate,
                maxDate
            };
        }
    }


</script>

<script setup>

    function SetWidgetConfig() {
        const temp = ref([ChosenGraph, Datamodel, ShowWhatData, GroupBy, StartDate, EndDate])
        widgets.value.push(temp.value)
        console.log(widgets)
        // widgets.push()
    }

</script>

<template>
    <div>
        <button v-if="test" @click="test = !test">+</button>

        <div v-if="!test">
            <div class="popup">
                <div class="popup-inner">
                    <div class="popup-content">
                        <slot />

                        <img id="my-image" @click="ChosenGraph = 'line-chart'; lineChartActive = true; barChartActive = false"
                             :class="{ 'active': lineChartActive }" src="../../src/images/line-chart.png" width="240" height="128" />

                        <img @click="ChosenGraph = 'bar-chart'; barChartActive = true; lineChartActive = false"
                             :class="{ 'active': barChartActive }" src="../../src/images/barchart.jpg" width="240" height="128" />
                        <br />
                        <select name="category" id="category" v-model="Datamodel">
                            <option disabled selected hidden value=""> Please choose what Datamodel u want to use</option>
                            <option v-for="item in listItems">{{item}}</option>
                        </select>
                        <br />
                        <label for="start-date">Start Date:</label><br />
                        <input type="date" id="start-date" v-model="StartDate" :min="minDate" :max="maxDate">
                        <br>
                        <label for="end-date">End Date:</label><br />
                        <input type="date" id="end-date" v-model="EndDate" :min="minDate" :max="maxDate">
                        <br />
                        <select name="category" id="category" v-model="GroupBy">
                            <option disabled selected hidden value="">Please choose what u want to sort by</option>
                            <option>Day</option>
                            <option>Week</option>
                            <option>Month</option>
                            <option>Year</option>
                        </select>
                        <br />
                        <select name="category" id="category" v-model="ShowWhatData">
                            <option disabled selected hidden value="">Please choose what kind of data u want to visualise</option>
                            <option>Count</option>
                        </select>
                        <br /><br />
                        <button @click="test = !test; SetWidgetConfig()">AddWidget</button>
                        <button @click="test = !test">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

        <table class="grid">
            <tbody>
                <tr v-for="row in 3" :key="row">
                    <td v-for="col in 3" :key="col"><img src="../../src/images/barchart.jpg"/></td>
                </tr>
            </tbody>
        </table>
    <div v-if="selectedChart === 'line-chart'">
        <div>{{selectedOption}}</div>
        <linegraph :my-string="selectedOption"></linegraph>
    </div>
    <div v-if="selectedChart === 'bar-chart'">
        <div>{{selectedOption}}</div>
        <bargraph :my-string="selectedOption"></bargraph>
    </div>
</template>

<style scoped>
    .grid {
        width: 100%;
        height: 100%;
        border-collapse: collapse;
        table-layout: fixed;
    }

        .grid td {
            border: 1px solid black;
            padding: 10px;
        }

    .active {
        border: 2px solid red;
    }
    .popup {
        position: fixed;
        top: 25%;
        left: 0;
        width: 100%;
        height: 100%;
        overflow-x: hidden;
        overflow-y: auto;
        z-index: 1;
    }

    .popup-inner {
        max-width: 30%;
        margin: 2rem auto;
    }

    .popup-content {
        background-color: lightgrey;
        position: relative;
        background-clip: padding-box;
        border-radius: 0.3rem;
        border: 1px solid rgba(0,0,0,0.3);
        padding: 1rem;
    }
</style>