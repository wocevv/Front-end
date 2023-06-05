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
    let selectedValue = ref("")
    const listgroupby = ref([])
    const Brands = ref([])
    const widgets = ref([])
    let ChosenName = ""



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
                    Brands.value = finalRes2
                    this.ListItems = this.ListItems.concat(finalRes2);
                    const resproducts = await fetch("https://localhost:5001/api/Widget/Products/Properties");
                    const finalResproducts = await resproducts.json();
                    this.Listgroupby = finalResproducts;
                    listgroupby.value = finalResproducts;
                  
                }
                else if (value === "Brands") {
                    const res = await fetch("https://localhost:5001/api/Widget/Brands/Properties");
                    const finalRes = await res.json();
                    this.Listgroupby = finalRes
                    listgroupby.value = finalRes;
                }
                else if (value === 'Datamodel') {
                    const res = await fetch("https://localhost:5001/api/Widget/Datamodels/Properties");
                    const finalRes = await res.json();
                    this.Listgroupby = finalRes
                    listgroupby.value = finalRes;
                }
                else {
                    listgroupby.value = []
                    this.ListItems = []
                }
                console.log(listgroupby)
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
        console.log(xaxis.value);
        console.log(listgroupby.value);

        const requestData = {
            DataOption: selectedValue.value,
            DataCategory: Datamodel,
            DataId: Datamodel.id,
            DataGrouper: xaxis.value,
            DataAction: ShowWhatData,
            DateStart: StartDate,
            DateEnd: EndDate,
            DateGrouper: GroupBy,
        };
     
        for (let i = 0; i < Brands.value.length; i++) {
            if (Datamodel === Brands.value[1]) {
                requestData.dataModel = "Brand";
            }
            else {
                requestData.dataModel = "Datamodel";
            }

        }
        if (!requestData.DateGrouper) {
            requestData.DateGrouper = 'empty';
        }

        console.log(requestData)
        for (let i = 0; i < listgroupby.value.length; i++) {
            if (xaxis.value === listgroupby.value[i]) {
                const queryParams = new URLSearchParams(requestData);
                axios
                    .post(`https://localhost:5001/api/widget/ApiModelTest2?${queryParams.toString()}`)
                    .then((response) => {
                        widgetdata.value = response.data;
                        console.log(widgetdata);
                    })
                    .catch((error) => {
                        console.error(error);
                    });
                    console.log("xas")
                    break
            } else if (i === listgroupby.value.length && xaxis.value !== listgroupby.value[i]){
                // Modify the requestData object for the else condition
                requestData.dataGrouper = yaxis.value;
                requestData.dataFilter = xaxis.value;
                console.log("yas")
                const queryParams = new URLSearchParams(requestData);
                axios
                    .post(`https://localhost:5001/api/widget/ApiModelTest2?${queryParams.toString()}`)
                    .then((response) => {
                        widgetdata.value = response.data;
                        console.log(widgetdata);
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
        <div>{{ChosenName}}</div>
        <bargraphtest :widgetdata="widgetdata"></bargraphtest>
    </div>
    <div>

        <div v-if="!addclicked">
            <div class="popup-container">
                <div class="popup">
                    <div class="popup-inner">
                        <div class="popup-content">
                            <slot />


                            <h2>Create New Widget</h2><br />
                            <label for="widget-title">Widget Title:</label><br />
                            <input type="text" id="name" v-model="ChosenName">

                            <br />
                            <label class="lblwidget" for="chart">Chart:</label><br />
                            <select  class="inpWidget" name="chart" id="chart" v-model="ChosenGraph">
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
                            <select class="inpWidget" name="category" id="category" v-model="Datamodel" :disabled="selectedValue === 'Datamodel' || selectedValue === 'Brands'">
                                <option v-for="datamodel in ListItems" v-bind:value="{id: datamodel.id}">{{datamodel.name}}</option>
                                <option v-if="selectedValue === 'Brands'" disabled selected hidden value="">All Brands have been chosen.</option>
                                <option v-if="selectedValue === 'Datamodel'" disabled selected hidden value="">All Datamodels have been chosen.</option>
                                <option v-if="selectedValue === 'Products'" disabled selected hidden value="">Please choose which Category you want to use.</option>
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



                            <div v-if="xaxis === 'created_at'">
                                <label class="lblwidget" for="start-date">Start Date:</label><br />
                                <input class="inpWidget" type="date" id="start-date" v-model="StartDate" :min="minDate" :max="maxDate">
                                <br>
                                <label class="lblwidget" for="end-date">End Date:</label><br />
                                <input class="inpWidget" type="date" id="end-date" v-model="EndDate" :min="minDate" :max="maxDate">
                                <br />
                                <div v-if="xaxis === 'created_at'">
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
        margin-right:3%;
    }

    .btnwidget:hover{
        background-color:black;
        color: white;
    }
</style>