<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                xmlns:user="urn:my-scripts">
  <msxsl:script language="C#" implements-prefix="user">
    <![CDATA[
        public string splitAndReturn(string datas, string reference)
        {
            string[] datasArray = datas.Split('|');
            string returnValue = string.Empty;
            foreach (string str in datasArray)
                if (str.Trim()!=string.Empty && str.Split('^')[3] == reference)
                    returnValue = str.Split('^')[4].Trim();
            return returnValue;            
        }      
        ]]>
  </msxsl:script>
  <xsl:output method="html" version="4.0" encoding="iso-8859-1" indent="yes"/>
  <xsl:param name="segmentName"></xsl:param>
  <xsl:param name="datas"></xsl:param>
  <!--Pour tests unitaires-->
  <!--    <xsl:variable name="segmentName">UNH</xsl:variable>
  <xsl:variable name="datas">UNH.0062.Message reference number.0062.431803|UNH.S009.MESSAGE IDENTIFIER.0065.DELINS|UNH.S009.MESSAGE IDENTIFIER.0052.3|UNH.S009.MESSAGE IDENTIFIER.0054.0|UNH.S009.MESSAGE IDENTIFIER.0051.OD|UNH.S009.MESSAGE IDENTIFIER.0057.|UNH.0068.Common access reference.0068.|UNH.S010.STATUS OF THE TRANSFER.0070.|UNH.S010.STATUS OF THE TRANSFER.0073.|</xsl:variable>-->
  <xsl:template match="/">
    <html>
      <head>
        <title>
          <xsl:value-of select="Message/Title"/>
        </title>
        <style type="text/css">
          .tableau{border: 1px solid #000000; width: 100%; border-spacing:0; border-collapse:collapse; text-align: right;}
          .tableau td{border: 1px solid #000000; padding: 5px;}
          .tableau th{background-color: #B0C4DE; color:#000000; border: 0px solid #000000; padding: 5px;}
          .tableau tr:hover{background-color: #FF6600;color:#FFFFFF !important;}
          .important{background-color: #FF6600;color:#FFFFFF; padding: 3px;}
          .important a{color:#FFFFFF}
          .normal{background-color: #FFFFFF; color: #000000}
          .Statut{color:#8B0000; font-weight:bold; text-align: center}
          .alignleft{text-align: left;}
          .alignCenter{text-align: center;}
          .spaceValue{width: 90%;}
        </style>
      </head>
      <body>
        <h1>
          <xsl:value-of select="Message/Title"/>
        </h1>
        <h2>
          Segment : <xsl:value-of select="//Enreg[Label=$segmentName][1]/Label"/>
          <xsl:variable name="MandatoryInfo">
            <xsl:choose>
              <xsl:when test="//Enreg[Label=$segmentName][1]/Statut='Y'">
                <xsl:text>Mandatory</xsl:text>
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>Optional</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          (<xsl:value-of select="$MandatoryInfo"/>/<xsl:value-of select="//Enreg[Label=$segmentName][1]/Repetitivite"/>)
        </h2>
        <h3>
          Description : <xsl:value-of select="//Enreg[Label=$segmentName][1]/Details"/>
        </h3>
        <xsl:apply-templates select="//Enreg[Label=$segmentName]" />
      </body>
    </html>
  </xsl:template>

  <xsl:template match="Enreg">
    <table class="tableau">
      <tr class="alignCenter">
        <th>
          Reference
        </th>
        <th>
          Label
        </th>
        <th>
          Statut
        </th>
        <th>
          Format
        </th>
        <th>
          Fields
        </th>
      </tr>
      <xsl:apply-templates select="./Zones/Zone"/>
    </table>
  </xsl:template>

  <xsl:template match="Zone">
    <tr class="cellule">
      <td class="alignCenter">
        <a href="#">
          <xsl:value-of select="./ZoneReference/text()"/>
        </a>
      </td>
      <td class="alignleft">
        <nobr>
          <xsl:value-of select="./ZoneLabel/text()"/>
        </nobr>
      </td>
      <td class="alignCenter">
        <xsl:value-of select="./ZoneStatut/text()"/>
      </td>
      <td class="alignleft">
        <xsl:value-of select="./ZoneFormat/text()"/>
      </td>
      <td class="alignleft">
        <xsl:choose>
          <xsl:when test="count(./Fields)>0">
            <table>
              <tr class="alignCenter">
                <th>
                  Reference
                </th>
                <th>
                  Label
                </th>
                <th>
                  Statut
                </th>
                <th>
                  Format
                </th>
                <th>
                  Value
                </th>
              </tr>
              <xsl:apply-templates select="./Fields/Field"/>
            </table>
          </xsl:when>
          <xsl:otherwise>
            <span class="important">
              <xsl:value-of select="user:splitAndReturn($datas,./ZoneReference/text())"/>
            </span>
          </xsl:otherwise>
        </xsl:choose>
      </td>
    </tr>
  </xsl:template>

  <xsl:template match="Field">
    <tr>
      <td class="alignCenter">
        <a href="#">
          <xsl:value-of select="./FieldReference/text()"/>
        </a>
      </td>
      <td class="alignleft">
        <nobr>
          <xsl:value-of select="./FieldLabel/text()"/>
        </nobr>
      </td>
      <td class="alignCenter">
        <xsl:value-of select="./FieldStatut/text()"/>
      </td>
      <td class="alignleft">
        <xsl:value-of select="./FieldFormat/text()"/>
      </td>
      <td class="alignleft">
        <span class="important">
          <xsl:value-of select="user:splitAndReturn($datas,./FieldReference/text())"/>
        </span>
      </td>
    </tr>
  </xsl:template>

</xsl:stylesheet>