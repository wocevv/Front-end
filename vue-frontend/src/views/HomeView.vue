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
    let Grouper = ""
    let xaxis = ref("")
    let dateGrouper = ""
    let xaxistest = ""
    let yaxis = ref("");
    const widgets = ref([])


    export default {
        name: 'Datepicker',
        data() {
            return {
                selectedValue: ref(""),
                options: ['Brands', 'Datamodel', 'Products'],
                addclicked: true,
                lineChartActive: false,
                barChartActive: false,
                ListItems: [],
                widgetdata: [],
                listfilters: ['Count', 'Sum'],
                Listgroupby: ref([]),
                yaxis,
                xaxis
               
            }
        },

        methods: {
            async getData(value) {      
                if (value === 'Products') {
                    const res = await fetch("https://localhost:5001/api/widget/Datamodel");
                    const finalRes = await res.json();
                    this.ListItems = finalRes;

                    const res2 = await fetch("https://localhost:5001/api/widget/Brands");
                    const finalRes2 = await res2.json();

                    this.ListItems = this.ListItems.concat(finalRes2);
                    const resproducts = await fetch("https://localhost:5001/api/Widget/Products/Properties");
                    const finalResproducts = await resproducts.json();
                    this.Listgroupby = finalResproducts;

                }
                else if (value === "Brands") {
                    const res = await fetch("https://localhost:5001/api/Widget/Brands/Properties");
                    const finalRes = await res.json();
                    this.Listgroupby = finalRes;
                }
                else if (value === 'Datamodel') {
                    const res = await fetch("https://localhost:5001/api/Widget/Datamodels/Properties");
                    const finalRes = await res.json();
                    this.Listgroupby = finalRes;
                }
                else {
                    this.ListItems = []
                }
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
        console.log(ShowWhatData)
        console.log(xaxis.value)
        const requestData = {      
            GraphType: ChosenGraph,
            dataModel: Datamodel.id,
            dataType: ShowWhatData,
            dateGrouper: GroupBy,
            DateStart: StartDate,
            DateEnd: EndDate,
            Grouper: xaxis.value,
        };
        if (!requestData.dateGrouper) {
            requestData.dateGrouper = 'empty',
                console.log(dateGrouper)
        };
        const queryParams = new URLSearchParams(requestData);
        axios.post(`https://localhost:5001/api/widget/ApiModelTest?${queryParams.toString()}`)
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
                            <select name="chart" id="chart" v-model="ChosenGraph">
                                <option disabled selected hidden value=""> Please choose which chart you want to use</option>
                                <option>Line Diagram</option>
                                <option>Bar Chart</option>
                            </select><br />


                            <label for="DataOption">Data option:</label><br />
                            <select v-model="selectedValue" @change="getData(selectedValue)">
                                <option disabled selected hidden value="">Please select a data option</option>
                                <option v-for="option in options" :value="option">{{option}}</option>
                            </select>
                            <br />

                            <label for="category">Category:</label><br />
                            <select name="category" id="category" v-model="Datamodel" :disabled="selectedValue === 'Datamodel' || selectedValue === 'Brands'">
                                <option v-for="datamodel in ListItems" v-bind:value="{id: datamodel.id}">{{datamodel.name}}</option>
                                <option v-if="selectedValue === 'Brands'" disabled selected hidden value="">All Brands have been chosen.</option>
                                <option v-if="selectedValue === 'Datamodel'" disabled selected hidden value="">All Datamodels have been chosen.</option>
                                <option v-if="selectedValue === 'Products'" disabled selected hidden value="">Please choose which Category you want to use.</option>
                                <option disabled selected hidden value="">Please choose Dataoption first.</option>
                            </select>
                            <br />
                            <label for="x-axis">X-Axis:</label><br />
                            <select name="chart" id="chart" v-model="xaxis">
                                <option disabled selected hidden value="">Please choose which x-axis you want to use</option>
                                <option disabled selected value="">*Group by*</option>
                                <option v-for="groupby in Listgroupby" :value="groupby">{{ groupby }}</option>
                                <option disabled value="">*Filters*</option>
                                <option v-for="filters in listfilters" :value="filters">{{ filters }}</option>
                            </select><br />

                            <label for="y-axis">Y-Axis:</label><br />
                            <select name="chart" id="chart" v-model="ShowWhatData">
                                <option disabled selected hidden value="">Please choose which y-axis you want to use</option>
                                <option v-for="groupby in Listgroupby" v-if="xaxis === '' || listfilters.includes(xaxis)">{{ groupby }}</option>
                                <option v-for="filters in listfilters" v-if="xaxis === '' || Listgroupby.includes(xaxis)">{{ filters }}</option>
                            </select><br />



                            <div v-if="xaxis === 'created_at'">
                                <label for="start-date">Start Date:</label><br />
                                <input type="date" id="start-date" v-model="StartDate" :min="minDate" :max="maxDate">
                                <br>
                                <label for="end-date">End Date:</label><br />
                                <input type="date" id="end-date" v-model="EndDate" :min="minDate" :max="maxDate">
                                <br />
                                <div v-if="xaxis === 'created_at'">
                                    <label for="date-category">Date Category:</label><br />
                                    <select name="category" id="category" v-model="GroupBy">
                                        <option disabled selected hidden value="">Please choose what u want to sort by</option>
                                        <option>Day</option>
                                        <option>Week</option>
                                        <option>Month</option>
                                        <option>Year</option>
                                    </select>
                                    <br />
                                </div>
                            </div>
                            <br />
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