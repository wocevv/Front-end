<script>

    let myString = "";
    import bargraphtest from '../../src/components/Bargraph.vue'
    import axios from 'axios';

    import router from '@/router'
    import { defineComponent, ref, watchEffect, computed } from 'vue'
    const widgetdata = ref([])
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
        data() {
            return {
                addclicked: true,
                lineChartActive: false,
                barChartActive: false,
                listItems: [],
                widgetdata: [],
                xaxis: '',
                yaxis: ''
               
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
            },
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
            },

        },
        setup(props) {
            if (props.selectedOption === null) {
                selectedOption = 'undefined'
            }
            if (props.selectedChart === null) {
                selectedChart = 'undefined'
            }
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
        const requestData = {
           
            GraphType: ChosenGraph,
            dataModel: Datamodel,
            dataType: ShowWhatData,
            dateGrouper: GroupBy,
            DateStart: StartDate,
            DateEnd: EndDate,
        };
        const queryParams = new URLSearchParams(requestData);
        axios.post(`https://localhost:5001/api/widget/ProductCount?${queryParams.toString()}`)
            .then((response) => {
                widgetdata.value = response.data;
                console.log(widgetdata);
            }).catch((error) => {
                console.error(error);
            });
    }

</script>

<template>
    <div v-if="widgetdata.length">
        <div>{{selectedOption}}</div>
        <bargraphtest :widgetdata="widgetdata"></bargraphtest>
    </div>
    <div>
        <button v-if="addclicked" @click="addclicked = !addclicked">+</button>

        <div v-if="!addclicked">
            <div class="popup-container">
                <div class="popup">
                    <div class="popup-inner">
                        <div class="popup-content">
                            <slot />
                            <h2>Create New Widget</h2><br />
                            <label for="widget-title">Widget Title:</label><br />
                            <input type="text" id="name" v-model="Name">
                            <br />
                            <label for="chart">Chart:</label><br />
                            <select name="chart" id="chart" v-model="Datamodel">
                                <option disabled selected hidden value=""> Please choose witch chart u want to use</option>
                                <option>Line Diagram</option>
                                <option>Bar Chart</option>
                            </select><br />
                            <label for="category">Category:</label><br />
                            <select name="category" id="category" v-model="ShowWhatData">
                                <option disabled selected hidden value=""> Please choose what Datamodel u want to use</option>
                                <option v-for="datamodel in listItems">{{datamodel}}</option>
                            </select>
                            <br />
                            <label for="x-axis">X-Axis:</label><br />
                            <select name="chart" id="chart" v-model="xaxis">
                                <option disabled selected hidden value=""> Please choose witch x-axis u want to use</option>
                                <option>Count</option>
                                <option>Group-by</option>
                                <option>Date</option>
                            </select><br />
                            <label for="y-axis">Y-Axis:</label><br />
                            <select name="chart" id="chart" v-model="yaxis">
                                <option disabled selected hidden value=""> Please choose witch x-axis u want to use</option>
                                <option>Count</option>
                                <option>Group-by</option>
                                <option>Date</option>
                            </select><br />
                            <div v-if="xaxis === 'Date' || yaxis === 'Date'">
                                <label for="start-date">Start Date:</label><br />
                                <input type="date" id="start-date" v-model="StartDate" :min="minDate" :max="maxDate">
                                <br>
                                <label for="end-date">End Date:</label><br />
                                <input type="date" id="end-date" v-model="EndDate" :min="minDate" :max="maxDate">
                                <br />
                                <label for="date-category">Date Category:</label><br />
                                <select name="category" id="category" v-model="GroupBy">
                                    <option disabled selected hidden value="">Please choose what u want to sort by</option>
                                    <option>Day</option>
                                    <option>Week</option>
                                    <option>Month</option>
                                    <option>Year</option>
                                </select>
                                <br />
                            </div><br />
                            <button @click="addclicked = !addclicked; SetWidgetConfig()">AddWidget</button>
                            <button @click="addclicked = !addclicked">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>

<style scoped>
    .active {
        border: 2px solid red;
    }
    .popup-container {
        position: fixed;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background-color: rgba(0, 0, 0, 0.5);
        display: flex;
        justify-content: left;
        align-items: center;
        z-index: 999;
    }
    .popup {
        width: 100%;
        max-width: 400px;
        height: 100%;
        
        background-color: #fff;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
        border-radius: 5px;
        overflow: hidden;
        transform: translateX(30%);
        transition: transform 0.3s ease-in-out;
    }

    .popup-inner {
        align-content:center;
        max-width: 90%;
        margin: 2rem auto;
    }

    .popup-content {
        background-color: #fff;
        position: relative;
        background-clip: padding-box;
        border-radius: 0.3rem;
        padding: 1rem;
    }
</style>