<script>

    import axios from 'axios';
    import router from '@/router'
    import { ref, computed } from 'vue'
    import bargraph from '../../src/components/Bargraph.vue'
    const widgetdata = ref([])
    let WidgetTitle = ""
    let Datamodel = ""
    let ShowWhatData = ref("")
    let GroupBy = ""
    let Datarange = ""
    let ChosenGraph = ""
    let StartDate = ""
    let EndDate = ""
    let Grouper = ""
    let xaxis = ref("")
    let dateGrouper = ""
    let yaxis = ref("");
    let selectedValue = ref("")
    let orientationValue = 0
    const listgroupby = ref([])
    const Brands = ref([])




    export default {
        name: 'Datepicker',
        data() {
            return {
                selectedValue: ref(""),
                options: ['All Brands', 'All Datamodels', 'Datamodel', 'Brand'],
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
                if (value === 'Datamodel') {
                    const res = await fetch("https://localhost:5001/api/widget/Datamodel");
                    const finalRes = await res.json();
                    this.ListItems = finalRes;

                    const resproducts = await fetch("https://localhost:5001/api/Widget/Products/Properties");
                    const finalResproducts = await resproducts.json();
                    this.Listgroupby = finalResproducts;
                    listgroupby.value = finalResproducts;
                  
                } else if (value === "Brand") {
                    const res = await fetch("https://localhost:5001/api/widget/Brands");
                    const finalRes = await res.json();
                    this.ListItems = finalRes;

                    const resproducts = await fetch("https://localhost:5001/api/Widget/Products/Properties");
                    const finalResproducts = await resproducts.json();
                    this.Listgroupby = finalResproducts;
                    listgroupby.value = finalResproducts;
                 
                }
                else if (value === "All Brands") {
                    const res = await fetch("https://localhost:5001/api/Widget/Brands/Properties");
                    const finalRes = await res.json();
                    this.Listgroupby = finalRes
                    listgroupby.value = finalRes;
                }
                else if (value === 'All Datamodels') {
                    const res = await fetch("https://localhost:5001/api/Widget/Datamodels/Properties");
                    const finalRes = await res.json();
                    this.Listgroupby = finalRes
                    listgroupby.value = finalRes;
                }
                else {
                    listgroupby.value = []
                    this.ListItems = []
                }
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
            orientationValue: {
                type: Number
            }

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
            DataOption: selectedValue.value,
            DataCategory: Datamodel,
            DataId: Datamodel.id,
            DataGrouper: xaxis.value,
            DataAction: ShowWhatData.value,
            DateStart: StartDate,
            DateEnd: EndDate,
            DateGrouper: GroupBy,
        };
        if (Datamodel.id === undefined) {
            requestData.DataId = "empty"
        }
        if (selectedValue.value !== "Products") {
            requestData.DataCategory = "empty"
        }
        console.log(requestData);
        for (let i = 0; i < Brands.value.length; i++) {
            if (Datamodel === Brands.value[1]) {
                requestData.DataCategory = "All Brands";
            }
            else {
                requestData.DataCategory = "All Datamodels";
            }

        }
        if (!requestData.DateGrouper) {
            requestData.DateGrouper = 'empty';
        }

        for (let i = 0; i < listgroupby.value.length; i++) {
            if (xaxis.value === listgroupby.value[i]) {
                const queryParams = new URLSearchParams(requestData);
                axios
                    .post(`https://localhost:5001/api/widget/ApiModelTest2?${queryParams.toString()}`)
                    .then((response) => {
                        widgetdata.value = response.data;
                        console.log(widgetdata);
                        orientationValue = 1;
                    })
                    .catch((error) => {
                        console.error(error);
                    });
                    break
            } else if (i === listgroupby.value.length - 1 && xaxis.value !== listgroupby.value[i]){
                requestData.DataGrouper = ShowWhatData.value;
                requestData.DataAction = xaxis.value;
                const queryParams = new URLSearchParams(requestData);
                axios
                    .post(`https://localhost:5001/api/widget/ApiModelTest2?${queryParams.toString()}`)
                    .then((response) => {
                        widgetdata.value = response.data;
                        console.log(widgetdata);
                        orientationValue = 2
                    })
                    .catch((error) => {
                        console.error(error);
                    });

            }
        }
    }



</script>

<template>
    <button id="btnAddWiget" v-if="addclicked" @click="addclicked = !addclicked">Add Widget</button>
    <div v-if="widgetdata.length">
        <h2>{{WidgetTitle}}</h2>
        <bargraph :widgetdata="widgetdata" :orientationValue="orientationValue" ></bargraph>
    </div>
    <div>

        <div v-if="!addclicked">
            <div class="popup-container">
                <div class="popup">
                    <div class="popup-inner">
                        <div class="popup-content">
                            <slot />


                            <h2 id="headText">Create New Widget</h2><br />
                            <label class="lblwidget" for="widget-title">Widget Title:</label><br />
                            <input class="inpWidget" type="text" id="name" v-model="WidgetTitle">

                            <br />
                            <label class="lblwidget" for="chart">Chart:</label><br />
                            <select class="inpWidget" name="chart" id="chart" v-model="ChosenGraph">
                                <option disabled selected hidden value=""> Please choose which chart you want to use</option>
                                <option>Line Diagram</option>
                                <option>Bar Chart</option>
                            </select><br />


                            <label class="lblwidget" for="DataOption">Data option:</label><br />
                            <select class="inpWidget" v-model="selectedValue" @change="getData(selectedValue)">
                                <option disabled selected hidden value="">Please select a data option</option>
                                <option v-for="option in options" :value="option">{{option}}</option>
                            </select>
                            <br />

                            <label class="lblwidget" for="category">Category:</label><br />
                            <select class="inpWidget" name="category" id="category" v-model="Datamodel" :disabled="selectedValue === 'Datamodels' || selectedValue === 'Brands'">
                                <option v-for="datamodel in ListItems" v-bind:value="{id: datamodel.id}">{{datamodel.name}}</option>
                                <option v-if="selectedValue === 'All Brands'" disabled selected hidden value="">All Brands have been chosen.</option>
                                <option v-if="selectedValue === 'All Datamodels'" disabled selected hidden value="">All Datamodels have been chosen.</option>
                                <option v-if="selectedValue === 'Datamodel'" disabled selected hidden value="">Please choose which Category you want to use.</option>
                                <option v-if="selectedValue === 'Brand'" disabled selected hidden value="">Please choose which brand you want to use.</option>
                                <option disabled selected hidden value="">Please choose Dataoption first.</option>
                            </select>
                            <br />
                            <label class="lblwidget" for="x-axis">X-Axis:</label><br />
                            <select class="inpWidget" name="chart" id="chart" v-model="xaxis">
                                <option disabled selected hidden value="">Please choose which x-axis you want to use</option>
                                <option disabled selected value="">*Group by*</option>
                                <option v-for="groupby in Listgroupby" :value="groupby">{{ groupby }}</option>
                                <option disabled value="">*Filters*</option>
                                <option v-for="filters in listfilters" :value="filters">{{ filters }}</option>
                            </select><br />

                            <label class="lblwidget" for="y-axis">Y-Axis:</label><br />
                            <select class="inpWidget" name="chart" id="chart" v-model="ShowWhatData">
                                <option disabled selected hidden value="">Please choose which y-axis you want to use</option>
                                <option v-for="groupby in Listgroupby" v-if="xaxis === '' || listfilters.includes(xaxis)">{{ groupby }}</option>
                                <option v-for="filters in listfilters" v-if="xaxis === '' || Listgroupby.includes(xaxis)">{{ filters }}</option>
                            </select><br />
                            <label class="lblwidget" for="start-date">Start Date:</label><br />
                            <input class="inpWidget" type="date" id="start-date" v-model="StartDate" :min="minDate" :max="maxDate">
                            <br>
                            <label class="lblwidget" for="end-date">End Date:</label><br />
                            <input class="inpWidget" type="date" id="end-date" v-model="EndDate" :min="minDate" :max="maxDate">
                            <br />
                                <div v-if="xaxis.includes('_at') || ShowWhatData.includes('_at') ">
                                    <label class="lblwidget" for="date-category">Date Category:</label><br />
                                    <select class="inpWidget" name="category" id="category" v-model="GroupBy">
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
                            <button class="btnwidget" @click="addclicked = !addclicked; SetWidgetConfig()">AddWidget</button>
                            <button class="btnwidget" @click="addclicked = !addclicked">Cancel</button>
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
        display: flex;
        justify-content: left;
        align-items: center;
        z-index: 999;
    }
    .popup {
        width: 100%;
        max-width: 400px;
        height: 100%;
        background-color: #1072E3;
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
        background-color: #1072E3e;
        position: relative;
        background-clip: padding-box;
        border-radius: 0.3rem;
        padding: 1rem;
    }

    #headText{
        color: white;
    }

    .lblwidget{
        color: white;
    }

    .inpWidget {
        box-sizing: border-box;
        width: 290px;
        height: 25px;
        background: #FFFFFF;
        border: 1px solid #000000;
        border-radius: 25px;
        margin-top: 1%;
        margin-bottom:2%;
    }

    .inpWidget:focus-visible{
        outline: none;
    }

    .btnwidget {
        box-sizing: border-box;
        width: 100px;
        height: 28px;
        background: #FFFFFF;
        border: 1px solid #000000;
        border-radius: 25px;
        margin-left:4.7%;
    }

    .btnwidget:hover{
        background-color:black;
        color: white;
    }
</style>